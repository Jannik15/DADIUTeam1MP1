using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class CSVReader : MonoBehaviour
{
    // Declaring varibales for the class:
    public string CSVPath = null;
    private string fileFullPath = null;

    private int labelAmount;
    private int quaternionAmount;
    private int positionAmount;
    private string[] labelValues;

    List<Quaternion>[] quaternionDatabase;
    List<Vector3>[] positionDatabase;
    List<float> timestampList;

    private void Awake()
    {
        ReadCSV();
    }

    void Start()
    {
        // Debug.Log(fileFullPath);    // Show path in log
        // ReadCSV();
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

    private void PositionInit()
    {
        positionAmount = ((labelAmount - 1) * 3 / 7) * 1 / 3; // Calculate amount of position vectors in CSV file
        positionDatabase = new List<Vector3>[positionAmount];

        for (int q = 0; q < positionAmount; q++)
        {
            positionDatabase[q] = new List<Vector3>();
        }

    }

    private List<Quaternion>[] QuaternionCreator(float[] data)
    {
        int n = 0;

        for (int i = 4; i < labelValues.Length; i += 7)   // Quaternion starts from position 4
        {

            Quaternion tempQuaternion = new Quaternion(data[i], data[i + 1], data[i + 2], data[i + 3]);
            // Debug.Log(data[i] + " " + data[i + 1] + " " + data[i + 2] + " " + data[i + 3]);  // For debugging (very performance heavy)
            quaternionDatabase[n].Add(tempQuaternion);

            n++;
        }
        // Debug.Log(quaternionDatabase[1][1]);

        return quaternionDatabase;
    }


    private List<Vector3>[] PositionCreator(float[] data)
    {
        int n = 0;

        for (int i = 1; i < labelValues.Length; i += 7)   // Vector starts from position 1
        {

            Vector3 tempPosition = new Vector3(data[i], data[i + 1], data[i + 2]);
            // Debug.Log(data[i] + " " + data[i + 1] + " " + data[i + 2]);  // For debugging (very performance heavy)
            positionDatabase[n].Add(tempPosition);

            n++;
        }

        return positionDatabase;
    }


    public void ReadCSV()
    {

        if (CSVPath != null || CSVPath == "")
        {

            fileFullPath = CSVPath;
            StreamReader strReader = new StreamReader(fileFullPath);

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

                    // Initialize the arrays and lists for quaternions, positions and timestamps:
                    QuaternionInit();
                    PositionInit();
                    TimestampInit();

                    // Debug.Log(labelValues[4]);  // For debugging
                    firstRun = false;

                }
                else
                {
                    string[] tempDataValues = dataString.Split(',');
                    // Debug.Log(tempDataValues[15]);

                    float[] dataValues = new float[tempDataValues.Length];
                    for (int i = 0; i < dataValues.Length; i++)
                    {
                        dataValues[i] = float.Parse(tempDataValues[i], CultureInfo.InvariantCulture.NumberFormat);

                    }

                    // Populate the quaternion, position and timestamp arrays/lists:
                    QuaternionCreator(dataValues);
                    PositionCreator(dataValues);
                    Timestamp(dataValues[0]);

                    // Ignore below, just for debugging:
                    // Debug.Log(QuaternionCreator(dataValues)[0][0]);
                    // Debug.Log(QuaternionCreator(dataValues)[2][0]);
                    // Debug.Log(dataValues[12]);
                }

            }
            Debug.Log("Motion Matching: pre-process completed!");
            IndexHelper(7);     //// <- USE the index helper to find the label for a selected index in the CSV
            // Debug.Log(quaternionDatabase[7][10]);

        } else
        {
            Debug.Log("Motion Mathing ERROR: path not correct, or CSV file not found!");
        }

    }

    public void IndexHelper(int index)
    {
        Debug.Log("CSV index " + index + " is the " + labelValues[index]);     

    }

    private void TimestampInit()
    {
        timestampList = new List<float>();
    }

    private void Timestamp(float timeStamp)
    {
        timestampList.Add(timeStamp);
    }

    public void ReadCSV(string fullPath)
    {
        fileFullPath = fullPath;
        ReadCSV();
    
    }

    /*
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
    } */
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
    public List<Quaternion>[] GetQuaternions()
    {
        if (quaternionDatabase != null)
        {
            return quaternionDatabase;
        }
        else
        {
            Debug.Log("Motion Matching Error: The quaternion database is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }   
    }

    public List<Vector3>[] GetPositions()
    {
        if (positionDatabase != null)
        {
            return positionDatabase;
        }
        else
        {
            Debug.Log("Motion Matching Error: The position database is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }
    }

    public List<float> GetTimestamps()
    {
        if (timestampList != null)
        {
            return timestampList;
        }
        else
        {
            Debug.Log("Motion Matching Error: The timestamp list is empty! - Please build it first using ReadCSV() and make sure you have set the path properly in the inspector.");
            return null;
        }
    }
}
