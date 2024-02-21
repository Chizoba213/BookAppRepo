using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Utilities
{
    public static class ServerResponseCode
    {
        public static string InvalidBookIdCode { get; } = "0005";
        public static string InvalidBookIdMessage { get;} = "Invalid Book ID";

        public static string InsufficientBookQunatityAvailableCode { get;} = "0007";

        public static string InsufficientBookQunatityAvailableMessage { get;} = "Insufficient Books";

        public static string BookOrderedSuccessfullyCode { get; } = "0009";
        public static string BookOrderedSuccessfullyMessage { get; } = "Book Ordered Successfully";



    }
}
