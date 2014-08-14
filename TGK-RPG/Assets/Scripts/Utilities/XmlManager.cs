using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

using System.Security.Cryptography;

public class XmlManager: MonoBehaviour
{
	public static bool ENABLE_ENCRYPTION = false;
	
	public static object LoadInstanceAsXml(string filename, System.Type type)
	{
		string xml = LoadXML(filename); 
		if((xml != null) && (xml.ToString() != ""))
		{ 
			// notice how I use a reference to System.Type here, you need this 
			// so that the returned object is converted into the correct type 
			return DeserializeObject(type, xml); 
		}
		return null;
	}
	
	public static void SaveInstanceAsXml(string filename, System.Type type, object instance)
	{
		string xml = SerializeObject(type, instance);
		SaveXML(filename, xml);
	}
	
	// Here we deserialize it back into its original form 
	private static object DeserializeObject(System.Type type, string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(type); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
		if(xmlTextWriter != null)
		{
			return xs.Deserialize(memoryStream);
		}
		return null;
	} 
	
	private static string SerializeObject(System.Type type, object instance)
	{
		string xmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(type); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, instance); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		xmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return xmlizedString; 
	} 
	
	private static void SaveXML(string filename, string xml) 
	{ 
		string path = Application.persistentDataPath + "/" + filename;
		
		StreamWriter writer; 
		FileInfo t = new FileInfo(path); 
		//Debug.Log(xml);
		
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else 
		{ 
			t.Delete(); 
			writer = t.CreateText(); 
		} 
		
		if (ENABLE_ENCRYPTION) xml = Encrypt(xml);
		
		writer.Write(xml); 
		writer.Close();
	} 
	
	private static string LoadXML(string filename)
	{
		string data = null;
		string path = Application.persistentDataPath + "/" + filename;
		//Debug.Log(path);
		
		FileInfo t = new FileInfo(path);
		if (t.Exists)
		{
			StreamReader sr = File.OpenText(path);
			if (sr != null) {
				data = sr.ReadToEnd();
				//Debug.Log(data);
				
				if (ENABLE_ENCRYPTION) 
					data = Decrypt(data);
			}
		}
		
		return data;
	}
	
	// the following methods came from the referenced URL
	private static string UTF8ByteArrayToString(byte[] characters) 
	{
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 
	
	private static byte[] StringToUTF8ByteArray(string pXmlString) 
	{
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 	
	
	private static string Encrypt(string toEncrypt)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
		
		// 256-AES key
		byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
		RijndaelManaged rDel = new RijndaelManaged();
		
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		
		rDel.Padding = PaddingMode.PKCS7;
		
		ICryptoTransform cTransform = rDel.CreateEncryptor();
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return System.Convert.ToBase64String (resultArray, 0, resultArray.Length);
	}
	
	private static string Decrypt(string toDecrypt)
	{
		byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("12345678901234567890123456789012");
		
		// AES-256 key
		byte[] toEncryptArray = System.Convert.FromBase64String (toDecrypt);
		RijndaelManaged rDel = new RijndaelManaged ();
		rDel.Key = keyArray;
		rDel.Mode = CipherMode.ECB;
		
		rDel.Padding = PaddingMode.PKCS7;
		
		// better lang support
		ICryptoTransform cTransform = rDel.CreateDecryptor ();
		
		byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
		
		return UTF8Encoding.UTF8.GetString (resultArray);
	}
} 