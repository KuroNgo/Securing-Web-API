import React from 'react';
import './App.css';
import { BrowserRouter, Route, Routes as Switch } from 'react-router-dom';
import Dashboard from './components/Dashboard/Dashboard';
import Preferences from './components/Preferences/Preferences';

function App() {
  return (
    <>
     <div className="wrapper">
      <h1>Application</h1>
      <BrowserRouter>
        <Switch>
          <Route path="/dashboard" element={<Dashboard/>}>
          </Route>
          <Route path="/preferences" element={<Preferences/>}>
          </Route>
        </Switch>
      </BrowserRouter>
    </div>
    </>
   
  );
}

export default App;