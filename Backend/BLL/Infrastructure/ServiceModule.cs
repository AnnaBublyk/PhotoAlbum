using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    /// <summary>Class ServiceModule.
    /// Implements the <see cref="Ninject.Modules.NinjectModule"/></summary>
    /// <seealso cref="Ninject.Modules.NinjectModule" />
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        /// <summary>Initializes a new instance of the <see cref="T:BLL.Infrastructure.ServiceModule"/> class.</summary>
        /// <param name="connection">The connection.</param>
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }

    }
}
