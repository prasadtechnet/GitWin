using PdfTemplator.PdfCore.Business.PdfModule;
using PdfTemplator.PdfCore.Models;
using PdfTemplator.PdfCore.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfTemplator.PdfCore.Business
{
    public interface IBusinessManager
    {
        ResponseModel GeneratePdfTemplate(TemplateModel objTemplate, string path);
    }
    public class BusinessManager : IBusinessManager
    {
        private FileModule.FileModule _file;
        public BusinessManager(FileModule.FileModule fileModule)
        {
            _file = fileModule;
        }
        public ResponseModel GeneratePdfTemplate(TemplateModel objTemplate, string path)
        {
            var objRes = new ResponseModel();
            try
            {
                var objPdfTemplate = PdfFactory.GetTemplate(objTemplate.Type);

                var respTemplate = objPdfTemplate.ProcessTemplate(objTemplate);

                if (respTemplate.Status)
                {
                    //template file
                    _file.WriteFile(path, respTemplate.PdfTemplateFileName, respTemplate.PdfTemplateFileContent);

                    //Models file if existis
                    if(!string.IsNullOrEmpty(respTemplate.PdfModelsFileName))
                        _file.WriteFile(path, respTemplate.PdfModelsFileName, respTemplate.PdfModelsFileContent);
                }
                else
                    return new ResponseModel {Status=false,Message=respTemplate.Message};
            }
            catch (Exception ex)
            {
                objRes.Status = false;
                objRes.Message = "Exp:" + ex.Message;
            }
            return objRes;
        }
    }
}
