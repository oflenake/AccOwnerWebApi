using Contracts;
using Entities;

namespace Repository
{
    /// <summary>
    /// The <see cref="RepositoryWrapper"/> class that is used by entity models and 
    /// their related entity models to access the backend repository base.
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper
    {
        #region Fields

        private ILoggerManager _logger;
        private string _component;
        private string _process;
        private string _message;

        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        #endregion


        public IOwnerRepository Owner
        {
            get
            {
                _process = "OwnerRepository";

                if (_owner == null)
                {
                    _owner = new OwnerRepository(_logger, _repoContext);

                    _message = string.Format($"{_component} Owner entity repository is 'null'. A new repository created " +
                                             $"and initialized at: '{_component}.{_process}', for the entity.");
                    _logger.LogWarn($"{_message}.");
                }

                _message = string.Format($"{_component} Owner entity repository wraped for further processing " +
                                         $"at: '{_component}.{_process}'");
                _logger.LogInfo($"{_message}.");

                return _owner;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                _process = "AccountRepository";

                if (_account == null)
                {
                    _account = new AccountRepository(_logger, _repoContext);

                    _message = string.Format($"{_component} Account entity repository is 'null'. A new repository created " +
                                             $"and initialized at: '{_component}.{_process}', for the entity.");
                    _logger.LogWarn($"{_message}.");
                }

                _message = string.Format($"{_component} Account entity repository wraped for further processing " +
                                         $"at: '{_component}.{_process}'");
                _logger.LogInfo($"{_message}.");

                return _account;
            }
        }

        public RepositoryWrapper(ILoggerManager logger, RepositoryContext repositoryContext)
        {
            _logger = logger;
            _repoContext = repositoryContext;

            _component = "RepositoryWrapper";
            _process = "RepositoryWrapper";
            _message = string.Format($"Initializing component: '{_component}', using its constructor: '{_component}.{_process}'.");

            _logger.LogInfo($"{_message}");
        }
    }
}
