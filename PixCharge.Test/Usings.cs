global using Xunit;
global using PixCharge.Domain.Account.Aggregates;
global using PixCharge.Domain.Account.ValueObject;
global using PixCharge.Domain.Core.Aggregates;
global using PixCharge.Domain.Core.Interfaces;
global using PixCharge.Domain.Core.ValueObject;
global using PixCharge.Domain.Transactions.Aggregates;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Conventions;
global using __mock__;
using Moq;
using PixCharge.Repository;
using System.Linq.Expressions;
using Castle.Core.Resource;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PixCharge.Application.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
public static class Usings
{
    public static Mock<IRepository<T>> MockRepositorio<T>(List<T> _dataSet) where T : BaseModel, new()
    {
        var _mock = new Mock<IRepository<T>>();

        _mock.Setup(repo => repo.Save(ref It.Ref<T>.IsAny));
        _mock.Setup(repo => repo.Update(ref It.Ref<T>.IsAny));
        _mock.Setup(repo => repo.Delete(It.IsAny<T>()));

        _mock.Setup(repo => repo.GetAll())
            .Returns(() => { return _dataSet.AsEnumerable(); });

        _mock.Setup(repo => repo.GetById(It.IsAny<Guid>()))
            .Returns(
            (Guid id) =>
            {
                return _dataSet.Single(item => item.Id == id);
            });


        _mock.Setup(repo => repo.Find(It.IsAny<Expression<Func<T, bool>>>()))
            .Returns(
            (Expression<Func<T, bool>> expression) =>
            {
                return _dataSet.Where(expression.Compile());
            });

        _mock.Setup(repo => repo.Exists(It.IsAny<Expression<Func<T, bool>>>()));
        return _mock;
    }

    public static Mock<DbSet<T>> MockDbSet<T>(List<T> data, DbContext? context = null)
where T : class
    {
        var queryableData = data.AsQueryable();
        var dbSetMock = new Mock<DbSet<T>>();
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        dbSetMock
            .As<IQueryable<T>>()
            .Setup(m => m.GetEnumerator())
            .Returns(() => queryableData.GetEnumerator());
        dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
        dbSetMock
            .Setup(d => d.Update(It.IsAny<T>()))
            .Callback<T>(item =>
            { // Atualizar a entidade no contexto
                var existingItem = data.FirstOrDefault(i => i == item);
                if (existingItem != null)
                {
                    context.Attach(item);

                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChangesAsync().Wait();
                }
            });
        dbSetMock.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(item => data.Remove(item));

        return dbSetMock;
    }
        public static string GenerateJwtToken(Guid userId)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var signingConfigurations = new SigningConfigurations();
        configuration.GetSection("TokenConfigurations").Bind(signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(signingConfigurations.Key.ToString())
        );
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim("UserId", userId.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: tokenConfigurations.Issuer,
            audience: tokenConfigurations.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(tokenConfigurations.Seconds),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}