using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            m_currentPlayer++;
            if(m_currentPlayer >= m_players.Count)
            {
                m_currentPlayer = 0;
            }
        }
    }
}
