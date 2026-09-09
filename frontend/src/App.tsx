function App() {
  const projectName = 'TaskHub'

  function handleTestClick() {
    alert('O React está respondendo aos eventos!')
  }

  return (
    <main>
      <p>Gerenciamento de projetos e tarefas</p>

      <h1>{projectName}</h1>

      <p>Frontend React funcionando.</p>

      <button type="button" onClick={handleTestClick}>
        Testar interação
      </button>
    </main>
  )
}

export default App