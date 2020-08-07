using RedSky.FileManagement.Contracts.Entities;

namespace RedSky.FileManagement.Tests.Stubs
{
    public class StubFileData
    {
        public FileData GetNew(int id)
        {
            return new FileData
            {
                Name = $@"TESTFILE_{id}",
                Id = id
            };
        }
    }
}
