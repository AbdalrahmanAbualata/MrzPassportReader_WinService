
namespace IdCardReader_WinService.Program
{
    using IdCardReader_WinService.Modilty;
    using Pr22;
    using Pr22.Processing;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    public class PassportAuth : IDisposable
    {
        private bool disposedValue;

        //DocumentReaderDevice pr = null;



        //----------------------------------------------------------------------
        /// <summary>
        /// Opens the first document reader device.
        /// </summary>
        /// <returns></returns>
        public int Open(DocumentReaderDevice pr)
        {
            pr.Connection += onDeviceConnected;
            pr.DeviceUpdate += onDeviceUpdate;

            try { pr.UseDevice(0); }
            catch (Pr22.Exceptions.General e)
            {
                throw e;
            }
            return 0;
        }
        //----------------------------------------------------------------------
        /// <summary>
        /// Returns a list of files in a directory.
        /// </summary>
        /// <param name="dirname"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public System.Collections.ArrayList FileList(string dirname, string mask)
        {
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            try
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(dirname);
                foreach (System.IO.DirectoryInfo d in dir.GetDirectories())
                    list.AddRange(FileList(dir.FullName + "/" + d.Name, mask));
                foreach (System.IO.FileInfo f in dir.GetFiles(mask))
                    list.Add(f.FullName);
            }
            catch (System.Exception) { }
            return list;
        }
        //----------------------------------------------------------------------
        /// <summary>
        /// Loads certificates from a directory.
        /// </summary>
        /// <param name="dir"></param>
        public void LoadCertificates(string dir ,DocumentReaderDevice pr)
        {
            string[] exts = { "*.cer", "*.crt", "*.der", "*.pem", "*.crl", "*.cvcert", "*.ldif", "*.ml" };
            int cnt = 0;

            foreach (string ext in exts)
            {
                System.Collections.ArrayList list = FileList(dir, ext);
                foreach (string file in list)
                {
                    try
                    {
                        BinData fd = new BinData().Load(file);
                        string pk = null;
                        if (ext == "*.cvcert")
                        {
                            //Searching for private key
                            pk = file.Substring(0, file.LastIndexOf('.') + 1) + "pkcs8";
                            if (!System.IO.File.Exists(pk)) pk = null;
                        }
                        if (pk == null)
                        {
                            pr.CertificateManager.Load(fd);
                            System.Console.WriteLine("Certificate " + file + " is loaded.");
                        }
                        else
                        {
                            pr.CertificateManager.Load(fd, new BinData().Load(pk));
                            System.Console.WriteLine("Certificate " + file + " is loaded with private key.");
                        }
                        ++cnt;
                    }
                    catch (Pr22.Exceptions.General)
                    {
                        System.Console.WriteLine("Loading certificate " + file + " is failed!");
                    }
                }
            }
            if (cnt == 0) System.Console.WriteLine("No certificates loaded from " + dir);
            System.Console.WriteLine();
        }
        //----------------------------------------------------------------------
        /// <summary>
        /// Does an authentication after collecting the necessary information.
        /// </summary>
        /// <param name="SelectedCard"></param>
        /// <param name="CurrentAuth"></param>
        /// <returns></returns>
        public bool Authenticate(ECard SelectedCard, Pr22.ECardHandling.AuthProcess CurrentAuth, string MrzFieldsString,List<AuthField> authFields)
        {

            BinData AdditionalAuthData = null;
            int selector = 0;
            switch (CurrentAuth)
            {
                case Pr22.ECardHandling.AuthProcess.BAC:
                case Pr22.ECardHandling.AuthProcess.PACE:
                case Pr22.ECardHandling.AuthProcess.BAP:
                    //Read MRZ (necessary for BAC, PACE and BAP)
                    //Pr22.Task.DocScannerTask ScanTask = new Pr22.Task.DocScannerTask();
                    //ScanTask.Add(Pr22.Imaging.Light.Infra);
                    //Page FirstPage = pr.Scanner.Scan(ScanTask, Pr22.Imaging.PagePosition.First);

                    //Pr22.Task.EngineTask MrzReadingTask = new Pr22.Task.EngineTask();
                    //MrzReadingTask.Add(FieldSource.Mrz, FieldId.All);
                    //Document MrzDoc = pr.Engine.Analyze(FirstPage, MrzReadingTask);

                    //AdditionalAuthData = new BinData().SetString(MrzDoc.GetField(FieldSource.Mrz, FieldId.All).GetRawStringValue());
                    AdditionalAuthData = new BinData().SetString(MrzFieldsString);
                    selector = 1;
                    break;

                case Pr22.ECardHandling.AuthProcess.Passive:
                case Pr22.ECardHandling.AuthProcess.Terminal:

                    //Load the certificates if not done yet
                    break;

                case Pr22.ECardHandling.AuthProcess.SelectApp:
                    if (SelectedCard.Applications.Count > 0) selector = (int)SelectedCard.Applications[0];
                    break;
            }

            AuthField authField = new AuthField();
            try
            {

                // Auth status 200 ok
              
                SelectedCard.Authenticate(CurrentAuth, AdditionalAuthData, selector);

                authField.FieldName = CurrentAuth.ToString();
                authField.FieldId = ((int)CurrentAuth);
                authField.AuthStatus = 200;
                authField.AuthError = "No Error";


                //System.Console.WriteLine("- " + CurrentAuth + " authentication succeeded");
                authFields.Add(authField);
                return true;
            }
            catch (Pr22.Exceptions.General e)
            {

                // Auth status 500 Failed



                authField.FieldName = CurrentAuth.ToString();
                authField.FieldId = ((int)CurrentAuth);
                authField.AuthStatus = 500;
                authField.AuthError = e.Message;

                //System.Console.WriteLine("- " + CurrentAuth + " authentication failed: " + e.Message);
                authFields.Add(authField);
                return false;
            }
           
        }
        //----------------------------------------------------------------------

        public string ReadMrz(Passport passport, DocumentReaderDevice pr, DocScanner Scanner, Engine OcrEngine)
        {
            //Devices can be manipulated only after opening.
            if (Open(pr) != 0) return "1";

            //Please set the appropriate path
            //LoadCertificates(pr.GetProperty("rwdata_dir") + "\\certs");
            //LoadCertificates(pr.GetProperty("rwdata_dir") + "\\certs",pr);


            //List<ECardReader> CardReaders = pr.Readers;
            

            //***** scan the IdCard and Read MRZ

            //DocScanner Scanner = pr.Scanner;
            //Engine OcrEngine = pr.Engine;


            //Connecting to the 1st card of any reader
            //ECard SelectedCard = null;
            //int CardCount = 0;
            //System.Console.WriteLine("Detected readers and cards:");
            //foreach (ECardReader reader in CardReaders)
            //{
            //    System.Console.WriteLine("\tReader: " + reader.Info.HwType);
            //    List<string> cards = reader.GetCards();
            //    if (SelectedCard == null && cards.Count > 0) SelectedCard = reader.ConnectCard(0);
            //    foreach (string card in cards)
            //    {
            //        System.Console.WriteLine("\t\t(" + CardCount++ + ")card: " + card);
            //    }
            //    System.Console.WriteLine();
            //}
            //if (SelectedCard == null)
            //{
            //    System.Console.WriteLine("No card selected!");
            //    return "2";
            //}



            string MrzFieldsString;

            try
            {
                System.Console.WriteLine("Scanning some images to read from.");
                Pr22.Task.DocScannerTask ScanTask = new Pr22.Task.DocScannerTask();
                //For OCR (MRZ) reading purposes, IR (infrared) image is recommended.
                ScanTask.Add(Pr22.Imaging.Light.White).Add(Pr22.Imaging.Light.Infra);
                Page DocPage = Scanner.Scan(ScanTask, Pr22.Imaging.PagePosition.First);


                //System.Console.WriteLine("Reading all the field data of the Machine Readable Zone.");
                Pr22.Task.EngineTask MrzReadingTask = new Pr22.Task.EngineTask();
                ////Specify the fields we would like to receive.
                MrzReadingTask.Add(FieldSource.All, FieldId.All);
                Document MrzDoc = OcrEngine.Analyze(DocPage, MrzReadingTask);

                //Image PassportImage = new Image();
                //passport.Image = PassportImage;
                //passport.Image.DataBytes = Scanner.GetPage(0).Select(Pr22.Imaging.Light.White).DocView().GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Jpeg).ToByteArray();


                //MrzDoc.GetField(FieldSource.Mrz, FieldId.All).GetRawStringValue()
                //MrzFieldsString = MrzDoc.GetField(FieldSource.Mrz, FieldId.All).GetRawStringValue();
                //MrzReadingTask.Add(FieldSource.Mrz, FieldId.All);
                //Document MrzDoc = OcrEngine.Analyze(DocPage, MrzReadingTask);


                PrintDocFields(MrzDoc, passport.DataPage.PassportFields);

            }
            catch (Pr22.Exceptions.General e)
            {
                throw e;
            }


            //"Executing authentications

            //Pr22.ECardHandling.AuthProcess CurrentAuth = SelectedCard.GetNextAuthentication(false);
            //bool PassiveAuthImplemented = false;


            //*************************************************************************************************
            //while (CurrentAuth != Pr22.ECardHandling.AuthProcess.None)
            //{
            //    if (CurrentAuth == Pr22.ECardHandling.AuthProcess.Passive) PassiveAuthImplemented = true;
            //    //if (CurrentAuth == Pr22.ECardHandling.AuthProcess.Terminal)
            //    //{
            //    //    CurrentAuth = SelectedCard.GetNextAuthentication(true);
            //    //    continue;
            //    //}
            //    bool authOk = Authenticate(SelectedCard, CurrentAuth,MrzFieldsString,passport.Authentication.AuthFields);
            //    CurrentAuth = SelectedCard.GetNextAuthentication(!authOk);
            //}

            //*************************************************************************************************
            //while (CurrentAuth != Pr22.ECardHandling.AuthProcess.None)
            //{
            //    if (CurrentAuth == Pr22.ECardHandling.AuthProcess.Passive)
            //    {
            //        CurrentAuth = SelectedCard.GetNextAuthentication(true);
            //        continue;
            //    }
            //    if (CurrentAuth == Pr22.ECardHandling.AuthProcess.Terminal)
            //    {
            //        CurrentAuth = SelectedCard.GetNextAuthentication(true);
            //        continue;
            //    }
            //    bool authOk = Authenticate(SelectedCard, CurrentAuth, MrzFieldsString);
            //    CurrentAuth = SelectedCard.GetNextAuthentication(!authOk);
            //}


            //System.Console.WriteLine("Reading data:");
            //List<Pr22.ECardHandling.File> FilesOnSelectedCard = SelectedCard.Files;
            //if (PassiveAuthImplemented)
            //{
            //    FilesOnSelectedCard.Add(Pr22.ECardHandling.FileId.CertDS);
            //    FilesOnSelectedCard.Add(Pr22.ECardHandling.FileId.CertCSCA);
            //}
            //foreach (Pr22.ECardHandling.File File in FilesOnSelectedCard)
            //{
            //    try
            //    {
            //        System.Console.Write("File: " + File + ".");
            //        BinData RawFileData = SelectedCard.GetFile(File);
            //        //RawFileData.Save(File + ".dat");
            //        Document FileData = pr.Engine.Analyze(RawFileData);
            //        //FileData.Save(Document.FileFormat.Xml).Save(File + ".xml");

            //        //Executing mandatory data integrity check for Passive Authentication
            //        if (PassiveAuthImplemented)
            //        {
            //            Pr22.ECardHandling.File f = File;
            //            if (f.Id >= (int)Pr22.ECardHandling.FileId.GeneralData)
            //                f = SelectedCard.ConvertFileId(f);
            //            if (f.Id >= 1 && f.Id <= 16)
            //            {
            //                System.Console.Write(" hash check...");
            //                System.Console.Write(SelectedCard.CheckHash(f) ? "OK" : "failed");
            //            }
            //        }
            //        //System.Console.WriteLine();
            //        PrintEcardDocFields(FileData,passport.Chip.PassportFields, passport.Chip);
            //    }
            //    catch (Pr22.Exceptions.General e)
            //    {
            //        System.Console.Write(" Reading failed: " + e.Message);
            //        //throw e;
            //    }
            //    //System.Console.WriteLine();
            //}

            //System.Console.WriteLine("Authentications:");
            //Document AuthData = SelectedCard.GetAuthResult();
            //PrintEcardDocFields(AuthData, passport.Authentication.PassportFields, passport.Chip);

            //SelectedCard.Disconnect();

            //pr.Close();
            return "3";
        }


        public void WriteMrzStatus(Document doc, List<PassportField> EcardFields, Chip eCard)
        {
        }

            //----------------------------------------------------------------------
            /// <summary>
            /// Prints out all fields of a document structure
            /// </summary>
            /// <remarks>
            /// Values are printed in three different forms: raw, formatted and standardized.
            /// Status (checksum result) is printed together with fieldname and raw value.
            /// At the end, images of all fields are saved into png format.
            /// </remarks>
            /// <param name="doc"></param>
            public void PrintEcardDocFields(Document doc, List<PassportField> EcardFields, Chip eCard)
        {
            System.Collections.Generic.List<FieldReference> Fields = doc.GetFields();

            foreach (FieldReference CurrentFieldRef in Fields)
            {

                PassportField field = new PassportField();
                try
                {
                    Pr22.Processing.Field CurrentField = doc.GetField(CurrentFieldRef);
                    string Value = "", FormattedValue = "", StandardizedValue = "";
                    byte[] binValue = null;
                    try
                    {
                        Value = CurrentField.GetRawStringValue();
                        field.Value = Value;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }
                    catch (Pr22.Exceptions.InvalidParameter) { binValue = CurrentField.GetBinaryValue(); }
                    try
                    {
                        FormattedValue = CurrentField.GetFormattedStringValue();
                        field.FormattedValuee = FormattedValue;

                    }
                    catch (Pr22.Exceptions.EntryNotFound)
                    {
                        //throw new Exception("bad");
                    }
                    try
                    {
                        StandardizedValue = CurrentField.GetStandardizedStringValue();
                        field.StandardizedValue = StandardizedValue;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }


                    Status Status = CurrentField.GetStatus();
                    field.Status = ((int)Status);


                    // 10 if it is ok
                    if (Status == Status.Ok)
                    {
                        field.Status =10;
                    }


                    string Fieldname = CurrentFieldRef.ToString();
                    field.Fieldname = Fieldname;

                    //if (Fieldname== "ECardName")
                    //{
                    //    String[] spearator = { "<<" };
                    //    string[] strlist = CurrentField.GetRawStringValue().Split(spearator, StringSplitOptions.None);
                    //    eCard.Surname = strlist[1];

                    //    String[] spearator2 = { "<" };
                    //    string[] strlist2 = strlist[0].Split(spearator2, StringSplitOptions.None);
                    //    eCard.FirstName = strlist2[0];
                    //    eCard.SecondName = strlist2[1];
                    //    eCard.ThirdName = strlist2[2];
                    //}

                    //if (Fieldname == "ECardOtherName")
                    //{
                    //    String[] spearator = { "<" };
                    //    string[] strlist = CurrentField.GetRawStringValue().Split(spearator, StringSplitOptions.None);
                    //    eCard.MotherNmae = strlist[0];
                    //    eCard.MotherFatherName= strlist[1];
                    //}


                    if (binValue != null)
                    {
                        //System.Console.WriteLine("  {0, -20}{1, -17}Binary", Fieldname, Status);
                        //for (int cnt = 0; cnt < binValue.Length; cnt += 16)
                        //    System.Console.WriteLine(PrintBinary(binValue, cnt, 16));
                    }
                    else
                    {

                        //if (CurrentFieldRef.ToString() == "ECardName")
                        //{

                        //}
                        //System.Console.WriteLine("  {0, -20}{1, -17}[{2}]", Fieldname, Status, Value);
                        //allData.AppendLine(Fieldname + ">>>>" + Status + ">>>>" + Value);
                        //allData.AppendLine("FormattedValue   >>>>>>>>>>>" + FormattedValue);
                        //allData.AppendLine("StandardizedValue   >>>>>>>>>>>" + StandardizedValue);
                        //System.Console.WriteLine("\t{1, -31}[{0}]", FormattedValue, "   - Formatted");
                        //System.Console.WriteLine("\t{1, -31}[{0}]", StandardizedValue, "   - Standardized");
                    }


                    List<int> detailedStatusList = new List<int>();
                    field.DetailedStatus = detailedStatusList;

                    List<Checking> lst = CurrentField.GetDetailedStatus();
                    foreach (Checking chk in lst)
                    {
                        field.DetailedStatus.Add((int)chk);
                        //System.Console.WriteLine(chk);
                    }

                    try
                    { // CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Png).Save(Fieldname + ".png");

                        byte[] imageByte = CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Jpeg).ToByteArray();
                        Image fieldImage = new Image();
                        field.Image = fieldImage;
                        field.Image.DataBytes = imageByte;

                        //allData.AppendLine(CurrentFieldRef.ToString() + "****************" + Convert.ToBase64String(CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Jpeg).ToByteArray()));
                    }
                    catch (Pr22.Exceptions.General) { }
                }
                catch (Pr22.Exceptions.General)
                {
                }
                EcardFields.Add(field);
            }
            //WriteInFile(allData.ToString());
            System.Console.WriteLine();

            foreach (FieldCompare comp in doc.GetFieldCompareList())
            {
                System.Console.WriteLine("Comparing " + comp.field1 + " vs. "
                    + comp.field2 + " results " + comp.confidence);
            }
            System.Console.WriteLine();

        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Prints out all fields of a document structure to console.
        /// </summary>
        /// <remarks>
        /// Values are printed in three different forms: raw, formatted and standardized.
        /// Status (checksum result) is printed together with fieldname and raw value.
        /// At the end, images of all fields are saved into png format.
        /// </remarks>
        /// <param name="doc"></param>
        static void PrintDocFields(Document doc, List<PassportField> DataPageFields)
        {
            System.Collections.Generic.List<FieldReference> Fields = doc.GetFields();

         

            foreach (FieldReference CurrentFieldRef in Fields)
            {

                PassportField field = new PassportField();
                try
                {
                    Pr22.Processing.Field CurrentField = doc.GetField(CurrentFieldRef);
                    string Value = "", FormattedValue = "", StandardizedValue = "";
                    byte[] binValue = null;
                    try
                    {
                        Value = CurrentField.GetRawStringValue();
                        field.Value = Value;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }
                    catch (Pr22.Exceptions.InvalidParameter) { binValue = CurrentField.GetBinaryValue(); }
                    try
                    {
                        FormattedValue = CurrentField.GetFormattedStringValue();
                        field.FormattedValuee = FormattedValue;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }
                    try
                    {
                        StandardizedValue = CurrentField.GetStandardizedStringValue();
                        field.StandardizedValue = StandardizedValue;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }
                    Status Status = CurrentField.GetStatus();
                    
                    field.Status = (int)Status;

                    if (Status == Status.Ok)
                    {
                        field.Status = 10;
                    }

                    string Fieldname = CurrentFieldRef.ToString();
                    field.Fieldname = Fieldname;
                    if (binValue != null)
                    {
                        //System.Console.WriteLine("  {0, -20}{1, -17}Binary", Fieldname, Status);



                        //for (int cnt = 0; cnt < binValue.Length; cnt += 16)
                        //System.Console.WriteLine(PrintBinary(binValue, cnt, 16));
                    }
                    else
                    {
                        //System.Console.WriteLine("  {0, -20}{1, -17}[{2}]", Fieldname, Status, Value);
                        //System.Console.WriteLine("\t{1, -31}[{0}]", FormattedValue, "   - Formatted");
                        //System.Console.WriteLine("\t{1, -31}[{0}]", StandardizedValue, "   - Standardized");

                        //System.Console.WriteLine(Fieldname + "---------->>>>>>>" + Status + "------->>>" + FormattedValue);
                    }

                    List<int> detailedStatusList = new List<int>();
                    field.DetailedStatus = detailedStatusList;

                    System.Collections.Generic.List<Checking> lst = CurrentField.GetDetailedStatus();
                    foreach (Checking chk in lst)
                    {

                        field.DetailedStatus.Add((int)chk);
                        //System.Console.WriteLine(chk);
                    }

                    //if (Fieldname == "VizFace")
                    //{
                    try
                    {
                        //System.Console.WriteLine(Convert.ToBase64String(CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Jpeg).ToByteArray())); }
                        // try { CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Png).Save(Fieldname + ".png"); }
                        //byte[] imageByte = CurrentField.GetImage().Save(Pr22.Imaging.RawImage.FileFormat.Jpeg).ToByteArray();
                        //Image fieldImage = new Image();
                        //field.Image = fieldImage;
                        //field.Image.DataBytes = imageByte;
                    }
                    catch (Pr22.Exceptions.General) { }
                    //}

                }
                catch (Pr22.Exceptions.General)
                {
                }
                DataPageFields.Add(field);
            }
  

            //foreach (FieldCompare comp in doc.GetFieldCompareList())
            //{
            //    System.Console.WriteLine("Comparing " + comp.field1 + " vs. "
            //        + comp.field2 + " results " + comp.confidence);
            //}
            
        }


        void onDeviceConnected(object a, Pr22.Events.ConnectionEventArgs e)
        {
            System.Console.WriteLine("Connection event. Device number: " + e.DeviceNumber);
        }
        //----------------------------------------------------------------------

        void onDeviceUpdate(object a, Pr22.Events.UpdateEventArgs e)
        {
            System.Console.WriteLine("Update event.");
            switch (e.part)
            {
                case 1:
                    System.Console.WriteLine("  Reading calibration file from device.");
                    break;
                case 2:
                    System.Console.WriteLine("  Scanner firmware update.");
                    break;
                case 4:
                    System.Console.WriteLine("  RFID reader firmware update.");
                    break;
                case 5:
                    System.Console.WriteLine("  License update.");
                    break;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~readRfidInformation()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}

