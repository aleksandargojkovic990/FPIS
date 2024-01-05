import React, { useState } from 'react';
import './App.css';
import Home from './components/Home';
import WineShop from './components/WineShop';

function App() {
  const [isHomePage, setIsHomePage] = useState(true);

  const handlePages = (isHomePage) => {
    setIsHomePage(isHomePage);
};

  return (
    <div>
      {isHomePage ? <Home handlePages = {handlePages} /> : <WineShop />}
  </div>
  );
}

export default App;
