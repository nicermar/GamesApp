using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using GMSTYS.Web.Models;
using GMSTYS.Data;
using GMSTYS.Model;
using System.Configuration;

namespace GMSTYS.Web.Controllers
{
    public class APIGamesToysController : ApiController
    {

        private Repository<GamesModel> Repository;

        //testing constructor
       
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            var Folder = ConfigurationManager.AppSettings["RepositoryFolder"];
            Repository = new Repository<GamesModel>(Folder);
            base.Initialize(controllerContext);

        }

        public APIGamesToysController()
        {
            var Folder = ConfigurationManager.AppSettings["RepositoryFolder"];
            Repository = new Repository<GamesModel>(Folder);
        }
        

        public IEnumerable<GamesToysViewModel> Get() 
        {
           
            try
            {
                var Games = Repository.GetAll();
                if (Games != null)
                {
                    IEnumerable<GamesToysViewModel> GamesViewModel = Games.Select(x => new GamesToysViewModel(x));
                    return GamesViewModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        
        public GamesToysViewModel GetById(int id)
        {
            GamesModel game = Repository.Get(id);
            if (game == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return new GamesToysViewModel(game);
        }

        public HttpResponseMessage PostGame(GamesToysViewModel game)
        {
            HttpResponseMessage response;
            bool isNew  = false;
            
            if(game.Id == 0)
            {
                isNew = true;
                var games = Repository.GetAll();
                game.Id = GetNextGameId(games);
                game.IsNew = true;
            }
            try
            {
                Repository.Add((GamesModel)game);
                if(isNew)
                {
                    response = Request.CreateResponse(HttpStatusCode.Created, game);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return response;
        }


        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {

            HttpResponseMessage response;
            if (Repository.Get(id) == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    Repository.Remove(id);
                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return response;
        }

        private int GetNextGameId(IEnumerable<GamesModel> games)
        {
            int nextId = 0;
            if (games != null )
            {
                if (games.Any())
                {
                    nextId = games.Max(x => x.Id) + 1;
                }
                else
                {
                    nextId = 1;
                }
            }
            else
            {
                nextId = 1;
            }
            return nextId;
        }

       
    }
}
