using System;
using System.Collections.Generic;
using System.Text;

namespace Warensoft.EntLib.StockServiceClient.Models
{
    public  class ResultInfoC
    {
        private bool operationDone;

        public bool OperationDone
        {
            get { return operationDone; }
            set
            {
                operationDone = value;
                 
            }
        }

        public DateTime LogTime { get; set; }

        private string message;

        public string Message
        {
            get { return message; }
            set
            {

                message = value;

            }
        }

        public int Code { get; set; }

        public ResultInfoC()
        {
            this.OperationDone = true;
        }
        
    }

    public class ResultInfoC<T> : ResultInfoC
    {

        public T AdditionalData { get; set; }
    }
}
