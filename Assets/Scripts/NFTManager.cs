using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class NFTManager : MonoBehaviour
{
    private class NFTs
    {
        public string contract { get; set; }
        public string tokenId { get; set; }
        public string uri { get; set; }
        public string balance { get; set; }
    }

    private class URI
    {
        public string name;
        public string description;
        public string image;
        public Attributes[] attributes;
    }

    private class Attributes
    {
        public string trait_type;
        public int value;
        public int max_value;
    }

    async void Start()
    {
        string chain = "ethereum";
        string network = "rinkeby"; // mainnet ropsten kovan rinkeby goerli
        string account = "0x25952eba25Ddb995226A72A320338B69CceBcf94";
        string contract = "0xC8f90241261c19901CfD9489Fa76D5460417f49d";
        int first = 500;
        int skip = 0;
        string response = await EVM.AllErc721(chain, network, account, contract, first, skip);
        try
        {
            NFTs[] erc721s = JsonConvert.DeserializeObject<NFTs[]>(response);

            foreach (var nft in erc721s)
            {
                var decodedBytes = Convert.FromBase64String(nft.uri.Substring(29));
                var decodedString = Encoding.UTF8.GetString(decodedBytes);
                var uri = JsonConvert.DeserializeObject<URI>(decodedString);

                print($"\"{uri.name}\" with attributes:");

                foreach (var attributeItem in uri.attributes)
                {
                    print($"Trait of type: \"{attributeItem.trait_type}\" with value of {attributeItem.value}");
                }
            }
        }
        catch
        {
            print("Error: " + response);
        }
    }
}