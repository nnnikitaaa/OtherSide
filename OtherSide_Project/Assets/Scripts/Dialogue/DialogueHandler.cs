using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueHandler : MonoBehaviour
{
    
    public float delayTime = 0.05f;
    public float defaultShowTime = 4f;
    public Dialogue dialogue;
    TextMeshPro textMeshPro;
    Queue<string> sentence;
    Queue<float> times;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        sentence = new Queue<string>();
        times = new Queue<float>();
        if (PlayerPrefs.GetInt("DoText") == 1 && dialogue) 
        {
            AddText(dialogue.showText);
            ShowText();
        }
    }
    //methods just to add text
    public void AddText(Dialogue.Sentence[] sentences)
    {
        for (int i = 0; i < sentences.Length; i++)
        {
            AddText(sentences[i].text, sentences[i].showTime);
        }
    }
    public void AddText(string text)
    {
        AddText(text, defaultShowTime);
    }
    public void AddText(string text, float showTime)
    {
        sentence.Enqueue(text);
        times.Enqueue(showTime);
    }
    //methods to add textImmidiatly
    public void AddAndShowTextNow(string text, float showTime)
    {
        sentence.Clear();
        times.Clear();
        AddText(text, showTime);
        ShowText();
    }
    public void AddAndShowTextNow(string text)
    {
        AddAndShowTextNow(text, defaultShowTime);
    }
    public void AddAndShowTextNow(Dialogue.Sentence[] sentences)
    {
        sentence.Clear();
        times.Clear();
        AddText(sentences);
        ShowText();
    }


    public void ShowText()
    {
        if (sentence.Count > 0)
        {
            StopAllCoroutines();
            StartCoroutine(ClearText(() =>
            {
                StartCoroutine(TypeText());
            }));
        }
    }
    IEnumerator TypeText()
    {
        string text = sentence.Dequeue();
        foreach (char letter in text.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(delayTime);
        }
        yield return new WaitForSeconds(times.Dequeue());

        int length = textMeshPro.text.ToCharArray().Length;
        if (sentence.Count > 0)
        {
            StartCoroutine(ClearText(() =>
            {
                StartCoroutine(TypeText());
            }));
        }
        else
        {
            StartCoroutine(ClearText());
        }
        
    }
    IEnumerator ClearText()
    {
        int length = textMeshPro.text.ToCharArray().Length;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                textMeshPro.text = textMeshPro.text.Remove(length - i - 1);
                yield return new WaitForSeconds(delayTime / 2);
            }
        }
    }
    IEnumerator ClearText(Action onDone)
    {
        int length = textMeshPro.text.ToCharArray().Length;
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                textMeshPro.text = textMeshPro.text.Remove(length - i - 1);
                yield return new WaitForSeconds(delayTime / 4);
            }
        }
        onDone?.Invoke();
    }
}
