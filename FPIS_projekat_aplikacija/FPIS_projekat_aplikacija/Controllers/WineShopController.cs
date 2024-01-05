using FPIS_projekat_aplikacija.DataTransferObjects;
using FPIS_projekat_aplikacija.Models;
using FPIS_projekat_aplikacija.SystemOperations;
using Microsoft.AspNetCore.Mvc;

namespace FPIS_projekat_aplikacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineShopController : ControllerBase
    {
        private IConfiguration _configuration;

        public WineShopController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddCart")]
        public JsonResult AddCart([FromBody] List<WineDTO> wines)
        {
            AddSO so = new AddSO(_configuration);

            List<CartItems> cartItems = new List<CartItems>();

            foreach (WineDTO wine in wines)
            {
                cartItems.Add(new CartItems()
                {
                    Wine = new Wine()
                    {
                        ID = wine.ID,
                        Price = wine.Price
                    },
                    Quantity = wine.Quantity,
                    Total = wine.Quantity * wine.Price
                });
            }

            try
            {
                so.ExecuteTemplate(new Cart()
                {
                    Total = cartItems.Sum(ci => ci.Quantity * ci.Wine.Price),
                    CartItems = cartItems
                });
                return new JsonResult("Uspešno kupljeno.");
            }
            catch (Exception)
            {
                return new JsonResult("Kupovina nije uspela. Kontaktirajte prodavca.");
            }
        }

        [HttpGet]
        [Route("GetWines")]
        public JsonResult GetWines()
        {
            GetSO<WineDTO> systemOperation = new GetSO<WineDTO>(_configuration);
            systemOperation.ExecuteTemplate(new Wine());
            return new JsonResult(systemOperation.Result);
        }

        [HttpGet]
        [Route("GetWineSorts")]
        public JsonResult GetWineSorts()
        {
            GetSO<WineSortDTO> systemOperation = new GetSO<WineSortDTO>(_configuration);
            systemOperation.ExecuteTemplate(new WineSort());
            return new JsonResult(systemOperation.Result);
        }

        [HttpGet]
        [Route("GetWineStyles")]
        public JsonResult GetWineStyles()
        {
            GetSO<WineStyleDTO> systemOperation = new GetSO<WineStyleDTO>(_configuration);
            systemOperation.ExecuteTemplate(new WineStyle());
            return new JsonResult(systemOperation.Result);
        }
    }
}
