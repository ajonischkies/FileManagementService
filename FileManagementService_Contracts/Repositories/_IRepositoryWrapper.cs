namespace RedSky.FileManagement.Contracts.Repositories
{
    public interface IRepositoryWrapper
    {
        IFileRepository Files { get; }
    }
}
