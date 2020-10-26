﻿using ImportFile.SharedKernel.Messaging;

namespace ImportFile.Core.Inventory.UseCases
{
    public static class ImportCsvFileUseCase
    {
        public class Arguments : ICommand
        {
            public string FileUrl { get; set; }
            public bool ContainsHeader { get; set; }
        }
    }
}