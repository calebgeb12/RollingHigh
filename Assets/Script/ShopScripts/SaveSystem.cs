using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveSystem
{


//save methods
    //for updating high score and coin count
    public static void savePointData(ballScript ballscript){
        PointData data = new PointData(ballscript);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pointData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();   
    }

    //specifically for doubling coin count
    public static void savePointData(ballScript ballscript, int coins){
        PointData data = new PointData(ballscript, coins);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pointData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();   
    }

    //for updating coin amount
    public static void savePointData(int newCoinAmount){
        PointData data = new PointData(newCoinAmount);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/pointData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();   
    }

    public static void saveShopData(ShopData data){ 
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/shopData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data); 
        stream.Close();   
    }

    public static void saveAbilitiesData(abilities data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/abilitiesData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();
    }

    public static void saveIAPData(IAPData data){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/IAPData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();
    }


//getter methods
    public static PointData getPointData(){
        string path = Application.persistentDataPath + "/pointData";
        if (File.Exists(path)){ 
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path, FileMode.Open);
            PointData data = formatter.Deserialize(stream) as PointData;
            stream.Close();
            return data;
        } 

        //the file doesn't exist, so it is created with a high score and coin total of zero
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            path = Application.persistentDataPath + "/pointData";
            FileStream stream = new FileStream(path, FileMode.Create);

            PointData data = new PointData(0, 0);

            formatter.Serialize(stream, data); 
            stream.Close();   

            return data;
        }
    }

    public static ShopData getShopData(){
        string path = Application.persistentDataPath + "/shopData";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path, FileMode.Open);
            // string fileContents = File.ReadAllText(path);
            // Debug.Log("File Contents:\n" + fileContents);
            ShopData data = formatter.Deserialize(stream) as ShopData; //error occurs at this point
            stream.Close();

            return data;
        } 

        //the file doesn't exist, so it is created with default zero values
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            ShopData data = new ShopData();

            formatter.Serialize(stream, data); 
            stream.Close();   
            return data;
        }
    }

    public static abilities getAbilitiesData(){
        string path = Application.persistentDataPath + "/abilitiesData";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path, FileMode.Open);
            // string fileContents = File.ReadAllText(path);
            // Debug.Log("File Contents:\n" + fileContents);
            abilities data = formatter.Deserialize(stream) as abilities; //error occurs at this point
            stream.Close();

            return data;
        } 

        //the file doesn't exist, so it is created with default zero values
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            abilities data = new abilities();

            formatter.Serialize(stream, data); 
            stream.Close();   
            return data;
        }
    }


    public static IAPData getIAPData(){
        string path = Application.persistentDataPath + "/IAPData";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path, FileMode.Open);
            // string fileContents = File.ReadAllText(path);
            // Debug.Log("File Contents:\n" + fileContents);
            IAPData data = formatter.Deserialize(stream) as IAPData; //error occurs at this point
            stream.Close();

            return data;
        } 

        //the file doesn't exist, so it is created with default zero values
        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            IAPData data = new IAPData();

            formatter.Serialize(stream, data); 
            stream.Close();   
            return data;
        }
    }

//reset methods
    public static void resetShopData(){
        string path = Application.persistentDataPath + "/shopData";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        ShopData data = new ShopData();

        formatter.Serialize(stream, data); 
        stream.Close();
    }

    public static void resetAbilitiesData(){
            string path = Application.persistentDataPath + "/abilitiesData";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            abilities data = new abilities();

            formatter.Serialize(stream, data); 
            stream.Close();   
    }

    public static void resetIAPData(){
            string path = Application.persistentDataPath + "/IAPData";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            IAPData data = new IAPData();

            formatter.Serialize(stream, data); 
            stream.Close();   
    }


//for intro
    public static void saveIntroData(){
        IntroPlayedVerifier data = new IntroPlayedVerifier(true);
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/introPlayData";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data); 
        stream.Close();   
    }

    public static IntroPlayedVerifier getIntroData(){
        string path = Application.persistentDataPath + "/introPlayData";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =  new FileStream(path, FileMode.Open);
            IntroPlayedVerifier data = formatter.Deserialize(stream) as IntroPlayedVerifier; //error occurs at this point
            stream.Close();

            return data;
        } 

        else{
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            IntroPlayedVerifier data = new IntroPlayedVerifier();

            formatter.Serialize(stream, data); 
            stream.Close();   
            return data;
        }
    }
}
 