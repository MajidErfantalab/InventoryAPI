using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Inventory.Domain.Models;

namespace Inventory.Application.Extentions;

public static class Extentions
{
    public static SGTIN96 TagSerializer(this string tag)
    {
        return new SGTIN96()
        {
            Header = tag,
            Filter = tag,
            Partition = tag,
            CompanyPerfix = tag,
            ItemReference = tag,
            SerialReference = tag
        };
    }
    
    public static BitArray HexToBinary(this string hexTag)
    {
        BitArray ba = new BitArray(4 * hexTag.Length);
        for (int i = 0; i < hexTag.Length; i++)
        {
            byte b = byte.Parse(hexTag[i].ToString(), NumberStyles.HexNumber);
            for (int j = 0; j < 4; j++)
                ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
        }
        return ba;
    }
    
        /// <summary>
        /// Method that tries to parse SGTIN-96 data from 24 hexadecimal digit RFID tag
        /// </summary>
        /// <param name="hexRfidTag">24 hexadecimal digit (96-bit) RFID tag containing SGTIN-96 data encoded</param>
        /// <param name="header">3-bit header value from SGTIN-96</param>
        /// <param name="filter">3-bit filter value from SGTIN-96</param>
        /// <param name="partition">8-bit partition value from SGTIN-96</param>
        /// <param name="companyPrefix">20–40-bit variable-length company-prefix value from SGTIN-96 (determined by partition)</param>
        /// <param name="itemReference">24-4-bit variable-length itemreference value from SGTIN-96 (determined by partition)</param>
        /// <param name="serialNumber">38-bit serial number</param>
        /// <param name="errorMessage">Error description, if error encountered during parsing</param>
        /// <returns></returns>
        public static bool TryParseSgtin96(this string hexRfidTag, out EnumHeader header, out EnumFilter filter, out EnumPartition partition, out long companyPrefix, out long itemReference, out long serialNumber, out string errorMessage)
        {
            header = default(EnumHeader);
            filter = default(EnumFilter);
            partition = default(EnumPartition);
            companyPrefix = default(long);
            itemReference = default(long);
            serialNumber = default(long);
            errorMessage = null;

            // RFID hexadecimal tags should be 24 digits (24 hex digits = 12 bytes = 96 bits)
            if (hexRfidTag.Length != 24)
            {
                errorMessage = "SGTIN-96 length not 96 bits";
                return false;
            }

            if (IsHex(hexRfidTag) == false)
            {
                errorMessage = "SGTIN-96 not valid hexadecimal";
                return false;
            }

            BitArray sgtinBitArr = HexToBinary(hexRfidTag);
            int bitsRead = 0;

            BitArray headerBitArr = new BitArray(sgtinBitArr.Cast<bool>().Take(8).ToArray());
            header = (EnumHeader)GetIntFromBitArray(ReverseArray(headerBitArr));
            bitsRead = 8;

            if (header == EnumHeader.Sgtin96)
            {
                BitArray filterBitArr = new BitArray(sgtinBitArr.Cast<bool>().Skip(bitsRead).Take(3).ToArray());
                filter = (EnumFilter)Convert.ToInt32(ConvertToByte(ReverseArray(filterBitArr)));
                bitsRead += 3;

                if (Enum.IsDefined(filter) == false)
                {
                    errorMessage = $"'filter' value {(int)filter} not valid";
                    return false;
                }

                BitArray partitionBitArr = new BitArray(sgtinBitArr.Cast<bool>().Skip(bitsRead).Take(3).ToArray());
                partition = (EnumPartition)GetIntFromBitArray(ReverseArray(partitionBitArr));
                bitsRead += 3;

                const int companyPrefixAndItemReferenceLength = 44;
                int companyPrefixLength = 0;
                switch (partition)
                {
                    case EnumPartition.Value0:
                        companyPrefixLength = 40;
                        break;

                    case EnumPartition.Value1:
                        companyPrefixLength = 37;
                        break;
                    case EnumPartition.Value2:
                        companyPrefixLength = 34;
                        break;

                    case EnumPartition.Value3:
                        companyPrefixLength = 30;
                        break;
                    case EnumPartition.Value4:
                        companyPrefixLength = 27;
                        break;

                    case EnumPartition.Value5:
                        companyPrefixLength = 24;
                        break;

                    case EnumPartition.Value6:
                        companyPrefixLength = 20;
                        break;

                    default:
                        errorMessage = $"'partition' value {partition} not valid";
                        return false;
                }

                BitArray companyPrefixBitArr = new BitArray(sgtinBitArr.Cast<bool>().Skip(bitsRead).Take(companyPrefixLength).ToArray());
                companyPrefix = GetIntFromBitArray(ReverseArray(companyPrefixBitArr));
                bitsRead += companyPrefixLength;

                var itemReferenceLength = companyPrefixAndItemReferenceLength - companyPrefixLength;

                BitArray itemReferenceBitArr = new BitArray(sgtinBitArr.Cast<bool>().Skip(bitsRead).Take(itemReferenceLength).ToArray());
                itemReference = GetIntFromBitArray(ReverseArray(itemReferenceBitArr));
                bitsRead += itemReferenceLength;

                BitArray serialNumberBitArr = new BitArray(sgtinBitArr.Cast<bool>().Skip(bitsRead).Take(38).ToArray());
                serialNumber = GetIntFromBitArray(ReverseArray(serialNumberBitArr));
            }
            else
            {
                errorMessage = $"'header' value {(int)header} not valid or not SGTIN-96";
                return false;
            }

            return true;
        }
     
     /// <summary>
     /// Check if string contains only hexadecimal digits
     /// </summary>
     /// <param name="chars"></param>
     /// <returns></returns>
     private static bool IsHex(IEnumerable<char> chars)
     {
         bool isHex;
         foreach (var c in chars)
         {
             isHex = ((c >= '0' && c <= '9') ||
                      (c >= 'a' && c <= 'f') ||
                      (c >= 'A' && c <= 'F'));

             if (!isHex)
                 return false;
         }
         return true;
     }
     
     private static BitArray ReverseArray(BitArray bits)
     {
         int len = bits.Count;
         BitArray a = new BitArray(bits);
         BitArray b = new BitArray(bits);

         for (int i = 0, j = len-1; i < len; ++i, --j)
         {
             a[i] = a[i] ^ b[j];
             b[j] = a[i] ^ b[j];
             a[i] = a[i] ^ b[j];
         }

         return a; 
     }
     
     private static byte ConvertToByte(BitArray bits)
     {
         byte[] bytes = new byte[1];
         bits.CopyTo(bytes, 0);
         return bytes[0];
     }
     
     private static double ConvertByteArrayToInt32(byte[] b)  
     {  
         return BitConverter.ToInt32(b, 0);  
     } 
     
     private static uint GetIntFromBitArray(BitArray bitArray)
     {

         ushort res = 0;
         for(int i= bitArray.Length-1; i != 0;i--)
         {
             if (bitArray[i])
             {
                 res = (ushort)(res + (ushort) Math.Pow(2, bitArray.Length- i -1));
             }
         }
         return res;
     }
}