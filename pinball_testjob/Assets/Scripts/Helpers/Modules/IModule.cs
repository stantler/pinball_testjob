namespace Helpers.Modules {
    public interface IModule {
        bool IsInitialized { get; set; }
        void Initialize();
        void Dispose();
    }
}
