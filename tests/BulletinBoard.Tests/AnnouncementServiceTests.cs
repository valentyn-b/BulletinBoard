using BulletinBoard.Core.Application.Dtos;
using BulletinBoard.Core.Application.Interfaces;
using BulletinBoard.Core.Application.Services;
using Moq;

namespace BulletinBoard.Tests
{
    public class AnnouncementServiceTests
    {
        private readonly Mock<IAnnouncementRepository> _mockRepo;
        private readonly AnnouncementService _service;

        public AnnouncementServiceTests()
        {
            _mockRepo = new Mock<IAnnouncementRepository>();

            _service = new AnnouncementService(_mockRepo.Object);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserIsAuthor_ShouldCallRepositoryDelete()
        {
            // Arrange
            int announcementId = 1;
            string authorId = "user-123";

            var mockAnnouncement = new AnnouncementDto
            {
                Id = announcementId,
                AuthorId = authorId
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(announcementId))
                     .ReturnsAsync(mockAnnouncement);

            // Act
            await _service.DeleteAsync(announcementId, authorId);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteAsync(announcementId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenUserIsNotAuthor_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            int announcementId = 1;
            string actualAuthorId = "user-123";
            string hackerId = "hacker-999";

            var mockAnnouncement = new AnnouncementDto
            {
                Id = announcementId,
                AuthorId = actualAuthorId
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(announcementId))
                     .ReturnsAsync(mockAnnouncement);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
                _service.DeleteAsync(announcementId, hackerId));

            Assert.Equal("You are not authorized to delete this announcement.", exception.Message);

            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_WhenAnnouncementDoesNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            int nonExistentId = 99;
            string userId = "user-123";

            _mockRepo.Setup(repo => repo.GetByIdAsync(nonExistentId))
                     .ReturnsAsync((AnnouncementDto?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.DeleteAsync(nonExistentId, userId));

            _mockRepo.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Never);
        }
    }
}