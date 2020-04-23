using Modelos;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers
{
    public class AnunciosController : ApiController
    {

        //GET api/anuncios
        public IHttpActionResult Get()
        {
            try
            {
                var condicao = new SQLBuilder();
                using (var cnx = new ConexaoBD())
                {
                    var lista = cnx.Anuncios.Listar(condicao);
                    return Ok(lista);
                }                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //GET api/anuncios/1
        public IHttpActionResult Get(int id)
        {
            try
            {
                var condicao = new SQLBuilder();
                condicao.AddWhere($"ID = {id}");
                using (var cnx = new ConexaoBD())
                {
                    var anuncio = cnx.Anuncios.Consultar(condicao);
                    return Ok(anuncio);
                }                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            
        }

        //POST api/anuncios
        public IHttpActionResult Post([FromBody]Anuncio value)
        {
            try
            {
                using (var cnx = new ConexaoBD())
                {
                    cnx.Anuncios.Salvar(value);
                    return Ok();
                }                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }            
        }

        //PUT api/anuncios/1
        public IHttpActionResult Put(int id, [FromBody]Anuncio value)
        {
            try
            {
                using (var cnx = new ConexaoBD())
                {
                    value.ID = id;
                    cnx.Anuncios.Salvar(value);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //DELETE api/anuncios/1
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var cnx = new ConexaoBD())
                {
                    var condicao = new SQLBuilder();
                    condicao.AddWhere($"ID = {id}");
                    var anuncio = cnx.Anuncios.Consultar(condicao);
                    cnx.Anuncios.Excluir(anuncio);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}