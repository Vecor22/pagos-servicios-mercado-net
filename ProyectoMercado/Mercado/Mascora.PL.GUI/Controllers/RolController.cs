//using Mercado.BL.BC;
//using Mercado.BL.BE;
//using Microsoft.AspNetCore.Mvc;

//namespace Mercado.PL.GUI.Controllers
//{
//    public class RolController : Controller
//    {
//        private readonly RolBC rolBC = new RolBC();

//        // GET: api/rol
//        [HttpGet]
//        public IActionResult Listar()
//        {
//            try
//            {
//                var lista = rolBC.Listar();
//                return Ok(lista);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, ex.Message);
//            }
//        }

//        // GET: api/rol/5
//        [HttpGet("{id}")]
//        public IActionResult BuscarPorId(long id)
//        {
//            try
//            {
//                var rol = rolBC.BuscarPorId(id);

//                if (rol == null)
//                    return NotFound("Rol no encontrado.");

//                return Ok(rol);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, ex.Message);
//            }
//        }

//        // POST: api/rol
//        [HttpPost]
//        public IActionResult Crear([FromBody] RolBE rol)
//        {
//            try
//            {
//                rolBC.Crear(rol);
//                return Ok("Rol creado correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        // PUT: api/rol
//        [HttpPut]
//        public IActionResult Actualizar([FromBody] RolBE rol)
//        {
//            try
//            {
//                rolBC.Actualizar(rol);
//                return Ok("Rol actualizado correctamente.");
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
