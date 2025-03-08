using GameCharacterVSA.Data;
using GameCharacterVSA.Entities;
using GameCharacterVSA.Features.CreateCharacter;
using GameCharacterVSA.Features.DeleteCharacter;
using GameCharacterVSA.Features.GetAllCharacters;
using GameCharacterVSA.Features.GetCharacterById;
using GameCharacterVSA.Features.LevelUpCharacter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task CreateCharacter_ShouldCreateCharacter()
        {
            // Arrange
            var mockDbContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var mediatorMock = new Mock<IMediator>();
            var request = new CreateCharacterRequest { Name = "TestCharacter", Class = "Warrior" };
            var command = new CreateCharacter.Command(request.Name, request.Class);
            var expectedCharacter = new GameCharacter { Name = request.Name, Class = request.Class, Level = 1 };

            mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(expectedCharacter);

            var endpoint = new CreateCharacterEndpoint(mockDbContext.Object, mediatorMock.Object);

            // Act
            var result = await endpoint.Create(command);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        // 测试获取所有角色功能
        [Fact]
        public async Task GetAllCharacters_ShouldReturnCharacters()
        {
            // Arrange
            var mockDbContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var mediatorMock = new Mock<IMediator>();
            var expectedCharacters = new List<GameCharacter>
            {
                new GameCharacter { Name = "Char1", Class = "Warrior" },
                new GameCharacter { Name = "Char2", Class = "Mage" }
            };

            mediatorMock.Setup(m => m.Send(new GetCharacters.Query(), It.IsAny<CancellationToken>())).ReturnsAsync(expectedCharacters);

            var endpoint = new GetAllCharactersEndpoint(mockDbContext.Object, mediatorMock.Object);

            // Act
            var result = await endpoint.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // 测试根据 ID 获取角色功能
        [Fact]
        public async Task GetCharacterById_ShouldReturnCharacter()
        {
            // Arrange
            var mockDbContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var mediatorMock = new Mock<IMediator>();
            var characterId = Guid.NewGuid();
            var expectedCharacter = new GameCharacter { Id = characterId, Name = "TestChar", Class = "Warrior" };

            mediatorMock.Setup(m => m.Send(new GetCharacter.Query(characterId), It.IsAny<CancellationToken>())).ReturnsAsync(expectedCharacter);

            var endpoint = new GetCharacterByIdEndpoin(mockDbContext.Object, mediatorMock.Object);

            // Act
            var result = await endpoint.GetById(characterId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // 测试角色升级功能
        [Fact]
        public async Task LevelUpCharacter_ShouldLevelUpCharacter()
        {
            // Arrange
            var mockDbContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var mediatorMock = new Mock<IMediator>();
            var characterId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(new LevelUpCharacter.Command(characterId), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var endpoint = new LevelUpCharacterEndpoint(mockDbContext.Object, mediatorMock.Object);

            // Act
            var result = await endpoint.LevelUp(characterId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // 测试删除角色功能
        [Fact]
        public async Task DeleteCharacter_ShouldDeleteCharacter()
        {
            // Arrange
            var mockDbContext = new Mock<DataContext>(new DbContextOptions<DataContext>());
            var mediatorMock = new Mock<IMediator>();
            var characterId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(new DeleteCharacter.Command(characterId), It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var endpoint = new DeleteCharacterEndpoint(mockDbContext.Object, mediatorMock.Object);

            // Act
            var result = await endpoint.Delete(characterId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
