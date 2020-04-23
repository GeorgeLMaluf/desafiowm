using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace Persistencia
{
    public class OnlineServices : IDisposable
    {        
        #region Constantes
        const string makesEndPoint = "api/OnlineChallenge/Make";
        const string modelsEndpoint = "api/OnlineChallenge/Model?MakeID=";
        const string versionsEndpoint = "api/OnlineChallenge/Version?ModelID=";
        #endregion

        HttpClient _client;

        #region Construtor
        public OnlineServices()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://desafioonline.webmotors.com.br/");
        }
        #endregion

        #region Metodos
        public async Task<IEnumerable<OnlineModel>> GetMakes()
        {            
            var response = _client.GetAsync(makesEndPoint).Result;
            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {                
                return await  JsonSerializer.DeserializeAsync<IEnumerable<OnlineModel>>(responseStream);
            }             
        }

        public async Task<IEnumerable<OnlineModel>> GetModels(int makeId)
        {
            var response = _client.GetAsync(modelsEndpoint + makeId).Result;
            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<OnlineModel>>(responseStream);
            }
        }

        public async Task<IEnumerable<OnlineModel>> GetVersions(int modelId)
        {
            var response = _client.GetAsync(versionsEndpoint + modelId).Result;
            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<OnlineModel>>(responseStream);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion
    }
}
