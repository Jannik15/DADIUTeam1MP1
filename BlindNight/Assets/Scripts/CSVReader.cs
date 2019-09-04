using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CSVReader : MonoBehaviour
{
    // Declaring varibales for the class:
    string fileName = null;
    string filePath = null;
    string fileFullPath = null;

    int labelAmount;
    int quaternionAmount;
    string[] labelValues;

    List<Quaternion>[] quaternionDatabase;
    List<Vector3>[] positionDatabase;

    void Start()
    {
        // Reading CSV should maybe be done on awake?
        // This is only for debugging:
        fileName = "InitialTestData.csv";
        filePath = "Assets/Resources/MotionMatchingData/";
        fileFullPath = filePath + fileName;
        // Debug.Log(fileFullPath);    // Show path in log
        ReadCSV();
    }

    private void QuaternionInit()
    {
        quaternionAmount = ((labelAmount - 1) * 4 / 7) * 1 / 4; // Calculate amount of quaternions in CSV file
        //  Debug.Log(labelAmount);
        //  Debug.Log(quaternionAmount);
        quaternionDatabase = new List<Quaternion>[quaternionAmount];


        for (int q = 0; q < quaternionAmount; q++)
        {
            quaternionDatabase[q] = new List<Quaternion>();
        }

    }

    private List<Quaternion>[] QuaternionCreator(float[] data)
    {

        int n = 0;

        for (int i = 4; i < labelValues.Length; i += 7)   // Quaternion starts from position 4
        {

            Quaternion tempQuaternion = new Quaternion(data[i], data[i + 1], data[i + 2], data[i + 3]);
            quaternionDatabase[n].Add(tempQuaternion);

            n++;
        }
        // Debug.Log(quaternionDatabase[1][1]);

        return quaternionDatabase;
    }


    private List<Vector3>[] PositionInit(float[] data)  // Not done YYY
    {
        positionDatabase = new List<Vector3>[1];

        return positionDatabase;
    }


    public void ReadCSV()
    {
        StreamReader strReader = new StreamReader(fileFullPath);

        try
        {
            strReader.ReadLine();
        }
        catch
        {
            Debug.Log("Error: Read CSV path not correct!");
        }
        bool endOfFile = false;
        bool firstRun = true;

        while (!endOfFile)
        {
            string dataString = strReader.ReadLine();

            if (dataString == null)
            {
                endOfFile = true;
                break;
            }

            if (firstRun)
            {
                string[] tempLabelValues = dataString.Split(',');   //
                labelAmount = tempLabelValues.Length;   //
                labelValues = tempLabelValues;

                QuaternionInit();   // First initialize the arrays and lists

                // Debug.Log(labelValues[4]);  // For debugging
                firstRun = false;

            } else
            {
                string[] tempDataValues = dataString.Split(',');
                // Debug.Log(tempDataValues[15]);

                float[] dataValues = new float[tempDataValues.Length];
                for (int i = 0; i < dataValues.Length; i++)
                {
                    dataValues[i] = float.Parse(tempDataValues[i], CultureInfo.InvariantCulture.NumberFormat); //
                }



                Debug.Log(QuaternionCreator(dataValues)[0][0]);
                Debug.Log(QuaternionCreator(dataValues)[1][0]);
                Debug.Log(QuaternionCreator(dataValues)[2][0]);
                // Debug.Log(dataValues[12]);
            }

        }

    }

    public void ReadCSV(string fullPath)
    {
        fileFullPath = fullPath;
        ReadCSV();
    
    }


    public void SetFilePath(string path)
    {
        filePath = path;
    }
    public string getFilePath()
    {
        if (filePath == null)
            Debug.Log("Error: File path is null!");

        Debug.Log(filePath);
        return filePath;
    }
    public void SetFileName(string file)
    {
        fileName = file;
    }
    public string GetFileName()
    {
        if (fileName == null)
            Debug.Log("Error: File name is null!");

        Debug.Log(fileName);
        return fileName;
    }
    public void SetFilePathFull(string fullPath)
    {
        fileFullPath = fullPath;
    }
    public string GetFilePathFull()
    {
        if (fileFullPath == null)
            Debug.Log("Error: The file path is null!");

        Debug.Log(fileFullPath);
        return fileFullPath;
    }
}
