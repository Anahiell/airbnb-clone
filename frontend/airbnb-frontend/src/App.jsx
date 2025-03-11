import { useState } from "react";
import { Routes, Route, useNavigate } from "react-router-dom";
import Header from "./components/Header";
import Footer from "./components/Footer";
import HomePage from "./pages/HomePage";
import ListingPage from "./pages/ListingPage";
import PropertyPage from "./pages/PropertyPage";
import ProfilePage from "./pages/ProfilePage";
import VerificationPage from "./pages/VerificationPage";
import Modal from "./components/Modal";
import SignInForm from "./components/SignInForm";
import { getUser, logout } from "./utils/auth";

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isMapVisible, setIsMapVisible] = useState(false); // Переключение карты
  const navigate = useNavigate();
  const user = getUser();

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div className="app">
      <Header 
        onOpenModal={() => !user && setIsModalOpen(true)} 
        user={user}
        onLogout={handleLogout}
        onToggleMap={() => setIsMapVisible(!isMapVisible)} // Переключаем карту
      />

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <SignInForm onClose={() => setIsModalOpen(false)} />
      </Modal>

      {/* Отображение карты, если включено */}
      {isMapVisible && <div className="mapContainer">ЗДЕСЬ БУДЕТ КАРТА</div>}

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/listings" element={<ListingPage />} />
        <Route path="/property/:id" element={<PropertyPage />} />
        <Route path="/profile" element={user ? <ProfilePage /> : <HomePage />} />
        <Route path="/verification" element={user ? <VerificationPage /> : <HomePage />} />
      </Routes>

      <Footer />
    </div>
  );
}

export default App;
