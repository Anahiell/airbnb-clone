import { useState } from "react";
import { Routes, Route, useNavigate } from "react-router-dom";
import Header from "./components/Header/Header";
import Footer from "./components/Footer/Footer";
import HomePage from "./pages/HomePage/HomePage";
import ListingPage from "./pages/ListingPage/ListingPage";
import PropertyPage from "./pages/PropertyPage/PropertyPage";
import ProfilePage from "./pages/ProfilePage/ProfilePage";
import VerificationPage from "./pages/VerificationPage/VerificationPage";
import Modal from "./components/Modal/Modal";
import SignInForm from "./components/SignInForm/SignInForm";
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
