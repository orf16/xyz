using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;

[assembly: OwinStartupAttribute(typeof(SG_SST.Startup))]

namespace SG_SST
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            ConfigurarHangFire(app);
        }

        private static void ConfigurarHangFire(IAppBuilder app)
        {
            // Configurar HangFire
            var pollTime = System.Configuration.ConfigurationManager.AppSettings["PollTimeHangFire"];
            var time = System.Configuration.ConfigurationManager.AppSettings["TimeHangFire"];
            int intPollTime;

            if (int.TryParse(pollTime, out intPollTime))
            {
                var options = new SqlServerStorageOptions
                {
                    QueuePollInterval = System.TimeSpan.FromHours(intPollTime)
                };
                SG_SST.Controllers.Organizacion.CompetenciaController competencia = new Controllers.Organizacion.CompetenciaController();
                GlobalConfiguration.Configuration.UseSqlServerStorage("SG_SSTContext", options);
                //BackgroundJob.Enqueue(() => System.Console.WriteLine("Fire-and-forgetIII!"))  //cola
                // BackgroundJob.Schedule(() => competencia.LeerExcel(), System.TimeSpan.FromSeconds(30));  //calendarizar
                //every 30 minutes
                // RecurringJob.AddOrUpdate(() => oc.SearchFinalDate(), "*/3 * * * *");

                //RecurringJob.AddOrUpdate(() => oc.SearchFinalDate(), "* * * * *");
                //at 15 minutes every hour every day
                //RecurringJob.AddOrUpdate(() => competencia.LeerExcel(), "15 * * * *");

                //at 00:00 every day
                RecurringJob.AddOrUpdate(() => competencia.LeerExcel(), time);
                app.UseHangfireDashboard();
                app.UseHangfireServer();
            }
            else
            {
                throw new System.ArgumentException("La clave PollTimeHangFire no tiene un valor entero valido.");
            }
        }
    }
}
