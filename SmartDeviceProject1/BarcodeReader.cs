using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SmartDeviceProject1
{
    public partial class BarcodeReader : Component
    {
        private Symbol.Barcode.BarcodeReader barcodeReader = null;

        public delegate void OnReadBarcodeReaderEventHandler(string Text);
        public event OnReadBarcodeReaderEventHandler OnRead;

        public BarcodeReader()
        {
            InitializeComponent();
            InitializeBarcodeReader();
        }

        public BarcodeReader(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            InitializeBarcodeReader();
        }

        private void InitializeBarcodeReader()
        {
            this.barcodeReader = new Symbol.Barcode.BarcodeReader();
            this.barcodeReader.ListChanged += new System.ComponentModel.ListChangedEventHandler(BarcodeReader_ListChanged);
            this.barcodeReader.Start();

            this.components.Add(this.barcodeReader);
        }

        void BarcodeReader_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            if (this.barcodeReader.ReaderData.Result == Symbol.Results.SUCCESS)
            {
                OnRead(this.barcodeReader.ReaderData.Text);
            }
        }
    }
}
