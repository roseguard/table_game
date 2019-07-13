using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    class PlayersManager
    {
        public static PlayersManager Instance { get; } = new PlayersManager();
        public List<Player> m_players = new List<Player>();
        public int m_currentPlayer = 0;

        PlayersManager()
        {
        }

        public void ClearPlayer()
        {
            m_players.Clear();
            m_currentPlayer = 0;
        }

        public int PlayersCount()
        {
            return m_players.Count;
        }

        public Player GetCurrentPlayer()
        {
            if(m_players.Count > m_currentPlayer)
            {
                return m_players[m_currentPlayer];
            }
            return null;
        }

        public void AddPlayer(Player player)
        {
            m_players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            m_players.Remove(player);
        }

        public void NextPlayer()
        {
            if (m_players[m_currentPlayer].TurnWasFinished())
            {
                m_players[m_currentPlayer].OnEndTurn();
                m_currentPlayer++;
                if (m_currentPlayer >= m_players.Count)
                {
                    m_currentPlayer = 0;
                }
                m_players[m_currentPlayer].OnStartTurn();
            }

            foreach(var player in m_players)
            {
                player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            }
            GetCurrentPlayer().GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        }
    }
}
