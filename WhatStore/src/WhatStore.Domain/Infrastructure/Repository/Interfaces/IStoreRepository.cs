﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatStore.Crosscutting.Infrastructure.Repository.Interfaces
{
    public interface IStoreRepository
    {
        Task<bool> UpdateStoreInformation(long userID, string storeName, string storeDescription, string phoneNumber, string email, string URL,
                                          string terms, bool hasAddress, string address, string number, string CEP, string complemento, int city);
    }
}