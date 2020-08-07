using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RedSky.FileManagement.Business.Services;
using RedSky.FileManagement.Contracts.Entities;
using RedSky.FileManagement.Contracts.Repositories;
using RedSky.FileManagement.Contracts.Services;
using RedSky.FileManagement.Tests.Mocks;
using RedSky.FileManagement.Tests.Stubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedSky.FileManagement.Tests.Services
{
    [TestClass]
    public class TestFileService
    {
        [TestMethod]
        public async Task FileServiceGetsAllFiles()
        {
            //Arrange
            int count = 2;
            IServiceWrapper services = SetupServiceWrapper(count);

            //Act
            IEnumerable<FileData> files = services.Files.GetAll();

            //Assert
            Assert.IsTrue(files.Count() > 0, $@"FileService.GetAll() should return {count} files:  Actual count is {files.Count()}");
        }

        [TestMethod]
        public async Task FileServiceSearchesFilesByName()
        {
            //Arrange
            string name = "TESTFILE_1";
            IServiceWrapper services = SetupServiceWrapper(2);

            //Act
            IEnumerable<FileData> files = services.Files.Search(file => file.Name == name);

            //Assert
            Assert.IsTrue(files.Count() > 0, $@"FileService.Search() should return a single match:  Actual count is {files.Count()}");
            Assert.IsTrue(files.First().Name == name, $@"FileService.Search() should return file with name {name}:  Actual name is {files.First().Name}");
        }

        [TestMethod]
        public async Task FileServiceSearchesFilesById()
        {
            //Arrange
            int id = 1;
            IServiceWrapper services = SetupServiceWrapper(2);

            //Act
            IEnumerable<FileData> files = services.Files.Search(file => file.Id == id);

            //Assert
            Assert.IsTrue(files.Count() > 0, $@"FileService.Search() should return a single match:  Actual count is {files.Count()}");
            Assert.IsTrue(files.First().Id == id, $@"FileService.Search() should return file with ID {id}:  Actual ID is {files.First().Id}");
        }

        [TestMethod]
        public async Task FileServiceAddsFile()
        {
            //Arrange
            int count = 1;
            int id = 2;
            IServiceWrapper services = SetupServiceWrapper(count);
            FileData newFile = new StubFileData().GetNew(id);

            //Act
            await services.Files.Add(newFile);
            IEnumerable<FileData> files = services.Files.GetAll();

            //Assert
            Assert.IsTrue(files.Count() > count, $@"FileService.GetAll() should return {count + 1} files:  Actual count is {files.Count()}");
        }

        [TestMethod]
        public async Task FileServiceDeletesFile()
        {
            //Arrange
            int count = 2;
            int id = 1;
            IServiceWrapper services = SetupServiceWrapper(count);

            //Act
            await services.Files.Delete(id);
            IEnumerable<FileData> files = services.Files.Search(file => file.Id == id);

            //Assert
            Assert.IsTrue(files.Count() == 0, $@"FileService.Search() should return no matches:  {files.Count()} matches found");
        }

        [TestMethod]
        public async Task FileServiceDeletesMultipleFiles()
        {
            //Arrange
            int count = 2;
            IServiceWrapper services = SetupServiceWrapper(count);
            IEnumerable<int> ids = services.Files.GetAll().Select(file => file.Id);

            //Act
            await services.Files.DeleteRange(ids);
            IEnumerable<FileData> files = services.Files.GetAll();

            //Assert
            Assert.IsTrue(files.Count() == 0, $@"FileService.GetAll() should return no entries:  {files.Count()} entries found");
        }

        private IServiceWrapper SetupServiceWrapper(int count)
        {
            Mock<IRepositoryWrapper> mockRepository = new Mock<IRepositoryWrapper>();
            mockRepository.Setup(_ => _.Files).Returns(new MockFileRepository(count));
            IServiceWrapper services = new ServiceWrapper(mockRepository.Object);

            return services;
        }
    }
}
