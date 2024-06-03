using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Reflection;

namespace lab_2_3
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            var connectionString = "Server=localhost;Port=5432;Database=post_service_lb_three;User Id=postgres;Password=password;";

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            
            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Dialect<NHibernate.Dialect.PostgreSQLDialect>();
                db.Driver<NHibernate.Driver.NpgsqlDriver>();
                db.LogSqlInConsole = true;
            });
            
            configuration.AddMapping(mapping);

            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
