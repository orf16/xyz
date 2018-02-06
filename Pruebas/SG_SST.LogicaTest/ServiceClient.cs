//using RestSharp;
//using SG_SST.EntidadesDominio.Planificacion;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;

//namespace SG_SST.ServiceRequest
//{
//    public class ServiceClient
//    {
//        private static Dictionary<string, object> _parametros = new Dictionary<string, object>();

//        public static void AdicionarParametro<T>(string key, T value)
//        {
//            _parametros.Add(key, value);
//        }

//        public static void EliminarParametros()
//        {
//            _parametros = new Dictionary<string, object>();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="urlBaseServicio"></param>
//        /// <param name="nombreCapacidad"></param>
//        /// <param name="tipoPeticion"></param>
//        /// <returns></returns>
//        /// 
//        public static T ObtenerObjetoJsonRestFul<T>(string urlBaseServicio, string nombreCapacidad, RestSharp.Method tipoPeticion)
//        {
//            T respuesta;
//            Uri baseUrl = new Uri(urlBaseServicio);
//            var cliente = new RestClient
//            {
//                BaseUrl = baseUrl
//            };

//            var request = new RestRequest(nombreCapacidad, tipoPeticion);
//            request.Parameters.Clear();
//            if (_parametros.Count > 0)
//            {
//                foreach (var parametro in _parametros)
//                {
//                    request.AddParameter(parametro.Key, parametro.Value);
//                }
//            }
//            request.AddHeader("Content-Type", "application/json");
//            request.AddHeader("Accept", "application/json");

//            IRestResponse response = cliente.Execute(request);
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                var result = response.Content;
//                respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
//            }
//            else
//                return default(T);
//            return respuesta;
//        }

//        /// <summary>
//        /// Consume un servicio rest en formato json para los verbos GET,
//        /// configurando los parámetros en la url
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="urlBaseServicio"></param>
//        /// <param name="nombreCapacidad"></param>
//        /// <param name="tipoPeticion"></param>
//        /// <param name="parametros"></param>
//        /// <returns></returns>
//        public static T[] ObtenerArrayJsonRestFul<T>(string urlBaseServicio, string nombreCapacidad, RestSharp.Method tipoPeticion)
//        {
//            T[] respuesta;
//            Uri baseUrl = new Uri(urlBaseServicio);
//            var cliente = new RestClient
//            {
//                BaseUrl = baseUrl
//            };

//            var request = new RestRequest(nombreCapacidad, tipoPeticion);
//            request.Parameters.Clear();
//            if (_parametros.Count > 0)
//            {
//                foreach (var parametro in _parametros)
//                {
//                    request.AddParameter(parametro.Key, parametro.Value);
//                }
//            }
//            request.AddHeader("Content-Type", "application/json");
//            request.AddHeader("Accept", "application/json");

//            IRestResponse response = cliente.Execute(request);
//            if (response.StatusCode == HttpStatusCode.OK)
//            {
//                var result = response.Content;
//                respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T[]>(result);
//            }
//            else
//                return new T[] { default(T) };
//            return respuesta;
//        }
            
//        /// <summary>
//        /// Cosume un servicio rest en formato json para los verbos POST, PUT
//        /// configurando un parámetro en el body del request y si existen, los
//        /// otros parámetros en la url
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="urlBaseServicio"></param>
//        /// <param name="nombreCapacidad"></param>
//        /// <param name="tipoPeticion"></param>
//        /// <param name="objeto"></param>
//        /// <returns></returns>
//        public static T RealizarPeticionesPostJsonRestFul<T>(string urlBaseServicio, string nombreCapacidad, T objeto)
//        {
//            T respuesta;
//            Uri baseUrl = new Uri(urlBaseServicio);
//            var cliente = new RestClient
//            {
//                BaseUrl = baseUrl
//            };
//            var request = new RestRequest(nombreCapacidad, Method.POST);
//            request.AddHeader("Accept", "application/json");
//            request.RequestFormat = DataFormat.Json;
//            request.Parameters.Clear();

//            if (_parametros.Count > 0)
//            {
//                foreach (var parametro in _parametros)
//                {
//                    request.AddParameter(parametro.Key, parametro.Value);
//                }
//            }
          
//            request.AddBody(objeto);
           

//            IRestResponse response = cliente.Execute(request);
//            if (response.StatusCode == HttpStatusCode.Created)
//            {
//                var result = response.Content;
//                respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
//            }
//            else
//                return default(T);
//            return respuesta;
//        }

//        public static T SolicitarGuaardadoCriterioPorCiclo<T>(string urlBaseServicio, string nombreCapacidad, EDEvaluacionEstandarMinimo objeto)
//        {
//            T respuesta;
//            Uri baseUrl = new Uri(urlBaseServicio);
//            var cliente = new RestClient
//            {
//                BaseUrl = baseUrl
//            };
//            var request = new RestRequest(nombreCapacidad, Method.POST);
//            request.AddHeader("Accept", "application/json");
//            request.RequestFormat = DataFormat.Json;
//            request.Parameters.Clear();
//            if (_parametros.Count > 0)
//            {
//                foreach (var parametro in _parametros)
//                {
//                    request.AddParameter(parametro.Key, parametro.Value);
//                }
//            }
//            //opcion 1
//            request.AddBody(objeto);
//            //opcion 3
//            //request.AddParameter("Empresa", objeto, ParameterType.RequestBody);

//            IRestResponse response = cliente.Execute(request);
//            if (response.StatusCode == HttpStatusCode.Created)
//            {
//                var result = response.Content;
//                respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
//            }
//            else
//                return default(T);
//            return respuesta;
//        }
//    }
//}