
import './App.css'
import { BrowserRouter as Router } from 'react-router-dom'
import AppRoutes from './routes/AppRoutes'
import Header from './components/Header'

function App() {
    return (
        <Router>
            <div>
                <Header />
                <AppRoutes />
            </div>
        </Router>
    )
}

export default App
