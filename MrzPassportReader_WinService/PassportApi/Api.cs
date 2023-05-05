using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Web.Http.Cors;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using IdCardReader_WinService.Modilty;
using System.Management;
using Pr22;
using Pr22.Exceptions;
using IdCardReader_WinService.Helper;
using IdCardReader_WinService.Program;

namespace IdCard_service_Api
{
    public class PassportApiController : ApiController
    {

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetMrzFromPassport()
        {

                string response = "null";
                Passport passport = new Passport();

                //Chip chip = new Chip();
                //passport.Chip = chip;

                DataPage dataPage = new DataPage();
                passport.DataPage = dataPage;

                //Authentication authentication = new Authentication();
                //passport.Authentication = authentication;

                //List<PassportField> chipFields = new List<PassportField>();
                //chip.PassportFields = chipFields;

                List<PassportField> DataPageFields = new List<PassportField>();
                dataPage.PassportFields = DataPageFields;

                //List<PassportField> authenticationFeilds = new List<PassportField>();
                //authentication.PassportFields = authenticationFeilds;

                //List<AuthField> authFields = new List<AuthField>();
                //authentication.AuthFields = authFields;


                DocumentReaderDevice pr = new DocumentReaderDevice();
                PassportAuth prog = new PassportAuth();


                DocScanner Scanner = pr.Scanner;
                Engine OcrEngine = pr.Engine;

                try
                {
                    response = prog.ReadMrz(passport, pr, Scanner, OcrEngine);
                    prog.Dispose();
                    Scanner.Dispose();
                    OcrEngine.Dispose();
                    pr.Dispose();
                    pr.Close();
                }
                catch (Pr22.Exceptions.General e)
                {
                    prog.Dispose();
                    Scanner.Dispose();
                    OcrEngine.Dispose();
                    pr.Dispose();
                    pr.Close();

                    Error error = new Error();

                      error.Message = e.Message;


                    if (ErrorCodes.ENOERR == e)
                    {
                        error.Code = ((int)ErrorCodes.ENOERR);
                        error.Message = "[pr22] Error is not specified!";
                    }
                    if (ErrorCodes.ENOENT == e)
                    {
                        error.Code = ((int)ErrorCodes.ENOENT);
                        error.Message = "Entry not found";
                    }
                    if (ErrorCodes.ENOMEM == e)
                    {
                        error.Code = ((int)ErrorCodes.ENOMEM);
                        error.Message = "Memory allocation error";
                    }
                    if (ErrorCodes.EACCES == e)
                    {
                        error.Code = ((int)ErrorCodes.EACCES);
                        error.Message = "Permission denied";
                    }
                    if (ErrorCodes.EFAULT == e)
                    {
                        error.Code = ((int)ErrorCodes.EFAULT);
                        error.Message = "Bad address or program error";
                    }
                    if (ErrorCodes.EBUSY == e)
                    {
                        error.Code = ((int)ErrorCodes.EBUSY);
                        error.Message = "Resource busy";
                    }

                    if (ErrorCodes.EEXIST == e)
                    {
                        error.Code = ((int)ErrorCodes.EEXIST);
                        error.Message = "File already exists";
                    }
                    if (ErrorCodes.ENODEV == e)
                    {
                        error.Code = ((int)ErrorCodes.ENODEV);
                        error.Message = "No such device";
                    }
                    if (ErrorCodes.EINVAL == e)
                    {
                        error.Code = ((int)ErrorCodes.EINVAL);
                        error.Message = "Invalid parameter";
                    }
                    if (ErrorCodes.ERANGE == e)
                    {
                        error.Code = ((int)ErrorCodes.ERANGE);
                        error.Message = "Data out of range";
                    }
                    if (ErrorCodes.EDATA == e)
                    {
                        error.Code = ((int)ErrorCodes.EDATA);
                        error.Message = "No data available";
                    }
                    if (ErrorCodes.ECOMM == e)
                    {
                        error.Code = ((int)ErrorCodes.ECOMM);
                        error.Message = "Communication error";
                    }
                    if (ErrorCodes.ETIMEDOUT == e)
                    {
                        error.Code = ((int)ErrorCodes.ETIMEDOUT);
                        error.Message = "Function timed out";
                    }
                    if (ErrorCodes.EOPEN == e)
                    {
                        error.Code = ((int)ErrorCodes.EOPEN);
                        error.Message = "File open error";
                    }
                    if (ErrorCodes.ECREAT == e)
                    {
                        error.Code = ((int)ErrorCodes.ECREAT);
                        error.Message = "File creation error";
                    }
                    if (ErrorCodes.EREAD == e)
                    {
                        error.Code = ((int)ErrorCodes.EREAD);
                        error.Message = " File read error";
                    }
                    if (ErrorCodes.EWRITE == e)
                    {
                        error.Code = ((int)ErrorCodes.EWRITE);
                        error.Message = "File write error";
                    }
                    if (ErrorCodes.EFILE == e)
                    {
                        error.Code = ((int)ErrorCodes.EFILE);
                        error.Message = "Invalid data content";
                    }
                    if (ErrorCodes.EINVIMG == e)
                    {
                        error.Code = ((int)ErrorCodes.EINVIMG);
                        error.Message = "Invalid image";
                    }
                    if (ErrorCodes.EINVFUNC == e)
                    {
                        error.Code = ((int)ErrorCodes.EINVFUNC);
                        error.Message = "Invalid function";
                    }
                    if (ErrorCodes.EHWKEY == e)
                    {
                        error.Code = ((int)ErrorCodes.EHWKEY);
                        error.Message = "Hardware key does not work properly";
                    }
                    if (ErrorCodes.EVERSION == e)
                    {
                        error.Code = ((int)ErrorCodes.EVERSION);
                        error.Message = "Invalid version";
                    }
                    if (ErrorCodes.EASSERT == e)
                    {
                        error.Code = ((int)ErrorCodes.EASSERT);
                        error.Message = "Assertion occurred";
                    }
                    if (ErrorCodes.EDISCON == e)
                    {
                        error.Code = ((int)ErrorCodes.EDISCON);
                        error.Message = "Device is disconnected";
                    }
                    if (ErrorCodes.EIMGPROC == e)
                    {
                        error.Code = ((int)ErrorCodes.EIMGPROC);
                        error.Message = " Image processing failed";
                    }
                    if (ErrorCodes.EAUTH == e)
                    {
                        error.Code = ((int)ErrorCodes.EAUTH);
                        error.Message = "Authenticity cannot be determined";
                    }
                    if (ErrorCodes.ECAPTURE == e)
                    {
                        error.Code = ((int)ErrorCodes.ECAPTURE);
                        error.Message = "Image capture error";
                    }
                    if (ErrorCodes.EWEAKDEV == e)
                    {
                        error.Code = ((int)ErrorCodes.EWEAKDEV);
                        error.Message = "Insufficient hardware configuration e.g. USB 1.0 port";
                    }
                    if (ErrorCodes.CERT_EXPIRED == e)
                    {
                        error.Code = ((int)ErrorCodes.CERT_EXPIRED);
                        error.Message = "Certificate is expired";
                    }
                    if (ErrorCodes.CERT_REVOKED == e)
                    {
                        error.Code = ((int)ErrorCodes.CERT_REVOKED);
                        error.Message = "Certificate is revoked";
                    }
                    if (ErrorCodes.ECHECK == e)
                    {
                        error.Code = ((int)ErrorCodes.ECHECK);
                        error.Message = "Validation checking failed";
                    }

                error.Path = e.StackTrace;

                    return Request.CreateResponse(HttpStatusCode.InternalServerError, error, Configuration.Formatters.JsonFormatter);
            }
            catch(Exception e)
            {
                prog.Dispose();
                Scanner.Dispose();
                OcrEngine.Dispose();
                pr.Dispose();
                pr.Close();

                Error error = new Error();
                error.Message = e.Message;
                error.Code = 222;
                error.Path = e.StackTrace;

                return Request.CreateResponse(HttpStatusCode.InternalServerError,error , Configuration.Formatters.JsonFormatter);
            }

                //if (response == "2")
                //{
                //    // 102 =No Passport Or chip issue
                //    //prog.Dispose();
                //    //pr.Dispose();
                //    //pr.Close();
                //    Error error = new Error();
                //    error.Code = 102;
                //    error.Message = "No Passport Or chip issue";
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError, error, Configuration.Formatters.JsonFormatter);
                //}

                return Request.CreateResponse(HttpStatusCode.OK, passport, Configuration.Formatters.JsonFormatter);          
        }


        //********************************************************************************************************************************************


        //********************************************************************************************************************************************

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage CheckServiceRuning()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "10001", Configuration.Formatters.JsonFormatter);
        }

    }
}
