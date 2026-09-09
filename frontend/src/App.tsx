import { useEffect, useState } from 'react'

type HealthResponse = {
  status: string
  service: string
  utcTime: string
}

function App() {
  const [health, setHealth] = useState<HealthResponse | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    async function loadHealth() {
      try {
        const response = await fetch('/api/health')

        if (!response.ok) {
          throw new Error(`A API respondeu com o status ${response.status}`)
        }

        const data: HealthResponse = await response.json()
        setHealth(data)
      } catch (error) {
        const message =
          error instanceof Error ? error.message : 'Erro desconhecido'

        setError(message)
      }
    }

    void loadHealth()
  }, [])

  return (
    <main>
      <p>Gerenciamento de projetos e tarefas</p>
      <h1>TaskHub</h1>

      {error && <p>Não foi possível acessar a API: {error}</p>}

      {!error && !health && <p>Verificando a API...</p>}

      {health && (
        <section>
          <p>
            Serviço: <strong>{health.service}</strong>
          </p>

          <p>
            Status: <strong>{health.status}</strong>
          </p>

          <p>
            Verificado em:{' '}
            <strong>
              {new Date(health.utcTime).toLocaleString('pt-BR')}
            </strong>
          </p>
        </section>
      )}
    </main>
  )
}

export default App