
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

    public class MrzPassport : IDisposable
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

        public string ReadMrz(Passport passport, DocumentReaderDevice pr, DocScanner Scanner, Engine OcrEngine)
        {
            //Devices can be manipulated only after opening.
            if (Open(pr) != 0) return "1";

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
                MrzReadingTask.Add(FieldSource.Mrz, FieldId.All);
                Document MrzDoc = OcrEngine.Analyze(DocPage, MrzReadingTask);

                System.Collections.Generic.List<FieldReference> Fields = MrzDoc.GetFields();
                // to check if the passport is on the reader or not
                if (Fields.Count < 7)
                {
                    //System.Console.WriteLine("Please flip the image to the another side ");
                    //System.Console.WriteLine();

                    return "2";

                }

                PrintDocFields(MrzDoc, passport);

            }
            catch (Pr22.Exceptions.General e)
            {
                throw e;
            }

            return "3";
        }


        //public void WriteMrzStatus(Document doc, List<PassportField> EcardFields, Chip eCard)
        //{
        //}


        /// <summary>
        /// Prints out all fields of a document structure to console.
        /// </summary>
        /// <remarks>
        /// Values are printed in three different forms: raw, formatted and standardized.
        /// Status (checksum result) is printed together with fieldname and raw value.
        /// At the end, images of all fields are saved into png format.
        /// </remarks>
        /// <param name="doc"></param>
        static void PrintDocFields(Document doc, Passport passport)
        {
            System.Collections.Generic.List<FieldReference> Fields = doc.GetFields();

         

            foreach (FieldReference CurrentFieldRef in Fields)
            {

                PassportField field = new PassportField();
                try
                {
                    Pr22.Processing.Field CurrentField = doc.GetField(CurrentFieldRef);
                    string Value = "", FormattedValue = "", StandardizedValue = "";
                    //byte[] binValue = null;
                    try
                    {
                        Value = CurrentField.GetRawStringValue();
                        field.Value = Value;
                    }
                    catch (Pr22.Exceptions.EntryNotFound) { }
                    catch (Pr22.Exceptions.InvalidParameter) { }
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
                    //Status Status = CurrentField.GetStatus();
                    
                    //field.Status = (int)Status;

                    //if (Status == Status.Ok)
                    //{
                    //    field.Status = 10;
                    //}

                    string Fieldname = CurrentFieldRef.ToString();
                    field.Fieldname = Fieldname;

                    // get surname 
                    if (Fieldname == "MrzSurname")
                    {
                        try
                        {
                            try { passport.Surname = CurrentField.GetFormattedStringValue(); } catch (Exception) { passport.Surname = ""; }
                        }
                        catch (Exception) { }
                    }

                    // get Givenname and seperate to  first second and third name .
                    if (Fieldname == "MrzGivenname")
                    {
                        try
                        {
                            String[] spearator = { " " };
                            string[] strlist = CurrentField.GetFormattedStringValue().Split(spearator, StringSplitOptions.None);
                                try { passport.FirstName = strlist[0]; } catch (Exception) { passport.FirstName = ""; }
                                try { passport.SecondName = strlist[1]; } catch (Exception) { passport.SecondName = ""; }
                                try { passport.ThirdName = strlist[2]; } catch (Exception) { passport.ThirdName = ""; }
                        }
                        catch (Exception) { }
                    }
                }
                catch (Pr22.Exceptions.General)
                {
                }
                passport.DataPage.PassportFields.Add(field);
            }
            
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

