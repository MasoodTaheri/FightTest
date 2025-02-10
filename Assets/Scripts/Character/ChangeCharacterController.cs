using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacterController : MonoBehaviour
{
    [SerializeField] private List<CharacterPresenter> _characterPresenters;
    [SerializeField] private CharacterPresenter _currentCharacterPresenter;
    private int _index = 0;
    [SerializeField] private GameObject _particle;
    [SerializeField] private float ChangeCharacterDelay;
    public Action CharcterIsReady;


    private void Awake()
    {
        foreach (var characterPresenter in _characterPresenters)
            characterPresenter.Hide();
    }

    public CharacterPresenter ChangeCharacter(int id)
    {
        StartCoroutine(ChangleCharacterParticle());
        if (_currentCharacterPresenter != null)
            _currentCharacterPresenter.Hide();
        _currentCharacterPresenter = _characterPresenters[id];
        _currentCharacterPresenter.Show();

        return _currentCharacterPresenter;
    }

    public CharacterPresenter NextCharacter()
    {
        _index++;
        if (_index >= _characterPresenters.Count)
            _index = 0;
        return ChangeCharacter(_index);
    }

    IEnumerator ChangleCharacterParticle()
    {
        _particle.SetActive(true);
        yield return new WaitForSeconds(ChangeCharacterDelay);
        _particle.SetActive(false);
        CharcterIsReady?.Invoke();
    }

    public int? FindHighestHealth()
    {
        int id = -1;
        float health = -1;
        for (int i = 0; i < _characterPresenters.Count; i++)
        {
            if (_characterPresenters[i].GetCurrentHealth() > health)
            {
                id = i;
                health = _characterPresenters[i].GetCurrentHealth();
            }
        }

        if (id != _index)
            return id;
        else
            return null;
    }
}