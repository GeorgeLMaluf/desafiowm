using Modelos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Api.Controllers
{
    public class ServicesController : ApiController
    {
        #region Variaveis Privadas
        private List<OnlineModel> _makes = new List<OnlineModel>();
        private List<OnlineModel> _models = new List<OnlineModel>();
        private List<OnlineModel> _versions = new List<OnlineModel>();
        #endregion

        #region Metodos
        public IEnumerable<String> GetMakes()
        {
            using (var srv = new OnlineServices())
            {
                this._makes = srv.GetMakes().Result.ToList();
                return this._makes.Select(m => m.Name);
            }
        }

        public IEnumerable<String> GetModels(string make)
        {
            using (var srv = new OnlineServices())
            {
                var curMake = srv.GetMakes().Result.ToList().FirstOrDefault(m => m.Name == make);
                if (curMake != null)
                {
                    this._models = srv.GetModels(curMake.ID).Result.ToList();
                }
                return this._models.Select(m => m.Name);
            }
                
        }

        public IEnumerable<String> GetVersions(string make, string model)
        {
            using (var srv = new OnlineServices())
            {
                var curMake = srv.GetMakes().Result.ToList().FirstOrDefault(m => m.Name == make);
                var curModel = srv.GetModels(curMake.ID).Result.ToList().FirstOrDefault(m => m.Name == model);                
                if (curModel != null)
                {
                    this._versions = srv.GetVersions(curModel.ID).Result.ToList();
                }
                return this._versions.Select(m => m.Name);
            }            
        }

        #endregion
    }
}