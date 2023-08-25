import React from 'react';
import { Route, Routes, BrowserRouter as Router } from 'react-router-dom';
import Layout from './Components/Layout.tsx';
import MainForm from './Components/MainForm';
import Header from './Components/Header';
import './App.css';

const App = () => {
    return (
        <Router>
            <section className="root">
                <Header />
                <Layout>
                    <Routes>
                        <Route path='/login' element={<MainForm />} />
                    </Routes>
                </Layout>
            </section>
        </Router>
    )
}

export default App;
