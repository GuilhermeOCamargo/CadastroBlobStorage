using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azure;
using FormCadastro.Models;

namespace FormCadastro.Controllers
{
    public class CadastroController : Controller
    {
        // GET: Cadastro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarPessoas()
        {
            
            return View();
        }
        public ActionResult ModalCadastro()
        {
            return PartialView("_ModalCadastro");
        }
        public ActionResult Tabela()
        {
            List<Pessoa> lista = new List<Pessoa>();
            return PartialView("_Tabela", lista);
        }
        public ActionResult Upload(Pessoa pessoa)
        {
            //INSERIR AQUI O MÉTODO PARA GUARDAR A PESSOA NO BANCO
            //O BANCO DEVE RETORNAR O ID DO REGISTRO QUE FOI INSERIDO
            //E BASEADO NO ID A IMAGEM DEVE SER RENOMEADA

            //ESSE É O MÉTODO PARA INSERIR A IMAGEM NO BLOBSTORAGE - NÃO ALTERAR NENHUM PARAMETRO(SÓ O NOME DA IMAGEM)
            //PARA PODER TESTAR, TEM QUE INSTALAR O EMULADOR DE BLOBSTORAGE DO AZURE NO COMPUTADOR
            // Modo Avançado
            if (Request.Files.Count == 0)
            {
                return Json("Sem arquivos!", JsonRequestBehavior.AllowGet);
            }

            if (Request.Files[0].ContentType != "image/jpeg")
            {
                return Json("Apenas imagens JPEG!", JsonRequestBehavior.AllowGet);
            }

            try
            {
                using (Image bmp = Image.FromStream(Request.Files[0].InputStream))
                {
                    if (bmp.Width > 2000 || bmp.Height > 2000)
                    {
                        return Json("A imagem deve ter dimensões menores do que 2000x2000!", JsonRequestBehavior.AllowGet);
                    }
                }

                Request.Files[0].InputStream.Position = 0;
            }
            catch
            {
                return Json("Imagem JPEG inválida!", JsonRequestBehavior.AllowGet);
            }

            AzureStorage.Upload(
                "web/",     //NOME DA PASTA
                "5.jpg",    //NOME DO ARQUIVO(RENOMEAR)
                Request.Files[0].InputStream,   //ARQUIVO
                "teste",    //NOME DO CONTAINER
                "UseDevelopmentStorage=true");  //URL PARA CONECTAR COM O BLOBSTORAGE - NÃO ALTERAR

            return Json("Sucesso!", JsonRequestBehavior.AllowGet);


        }
    }
}