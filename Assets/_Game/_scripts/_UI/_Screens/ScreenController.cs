using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private ScreenType m_type;
    public ScreenType Type => m_type;

    [SerializeField] private View m_mainView;
    [SerializeField] private List<View> m_views = new();

    public void OpenScreen()
    {
        m_views.Add(m_mainView);
        m_mainView.Open();
    }

    public void CloseScreen()
    {
        m_views.Last().Close();
        m_views.Clear();
    }

    public void OpenView(View view)
    {
        if(m_views.Last() != null){
            m_views.Last().Hide();
        }

        view.Open();
        m_views.Add(view);
    }

    public void CloseLastView()
    {
        var lastView = m_views.Last();
        lastView.Close();
        lastView.Entity_OnClosed += () =>
        {
            m_views.Last().Open();
        };
    }

    public void OnViewHasOpened(View view)
    {
        m_views.Add(view);
    }
}
