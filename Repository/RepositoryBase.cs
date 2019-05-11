using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// The <see cref="RepositoryContext"/> protected DBContext field is the main member of the 
    /// main generic <see cref="RepositoryBase"/> abstract class. This class is the base data 
    /// repository access layer inherited and used by all other data repositories, through the 
    /// <see cref="RepositoryWrapper"/> class, to access the entity models and 
    /// their related entity models' backend database.
    /// </summary>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        #region Fields

        protected ILoggerManager _logger;
        protected string _component;
        protected string _process;
        protected string _message;
        #endregion

        #region Properties

        protected RepositoryContext RepositoryContext { get; set; }
        #endregion

        #region Constructor

        public RepositoryBase(ILoggerManager logger, RepositoryContext repositoryContext)
        {
            this._logger = logger;
            this.RepositoryContext = repositoryContext;

            this._component = "RepositoryBase";
            this._process = "RepositoryBase";
            this._message = string.Format($"Initializing component: '{this._component}', using its " +
                                          $"constructor: '{this._component}.{this._process}'.");
            this._logger.LogInfo($"{this._message}");
        }
        #endregion

        #region Public Methods

        public IQueryable<T> GetAllBaseData()
        {
            this._process = "GetAllBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is retrieving all {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            return this.RepositoryContext.Set<T>();
        }

        public IQueryable<T> GetByIDBaseData(Expression<Func<T, bool>> expression)
        {
            this._process = "GetByIDBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is retrieving by id {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            return this.RepositoryContext.Set<T>()
                .Where(expression);
        }

        public void PostCreateBaseData(T entity)
        {
            this._process = "PostCreateBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is creating and adding a new {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void PutUpdateBaseData(T entity)
        {
            this._process = "PutUpdateBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is updating {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void DeleteByIDBaseData(T entity)
        {
            this._process = "DeleteByIDBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is deleting by id {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsyncBaseData()
        {
            this._process = "SaveAsyncBaseData";
            this._message = string.Format($"{this._component} queryable method '{this._component}.{this._process}' " +
                                          $"is asynchronously saving {this.RepositoryContext.Set<T>()} entitie's data, using " +
                                          $"the base repository database");
            this._logger.LogInfo($"{this._message}.");

            await this.RepositoryContext.SaveChangesAsync();
        }
        #endregion
    }
}
