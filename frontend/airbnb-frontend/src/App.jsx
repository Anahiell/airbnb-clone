import { useState } from "react";
import { Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Footer from "./components/Footer";
import HomePage from "./pages/HomePage";
import ListingPage from "./pages/ListingPage";
import PropertyPage from "./pages/PropertyPage";
import ProfilePage from "./pages/ProfilePage";
import Modal from "./components/Modal";
import SignInForm from "./components/SignInForm";
import RegisterForm from "./components/RegisterForm";

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isRegister, setIsRegister] = useState(false);

  return (
    <div className="app">
      <Header onOpenModal={() => { 
        setIsModalOpen(true);
        setIsRegister(false);
      }} />

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        {isRegister ? (
          <RegisterForm onToggleLogin={() => setIsRegister(false)} />
        ) : (
          <SignInForm onToggleRegister={() => setIsRegister(true)} />
        )}
      </Modal>

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/listings" element={<ListingPage />} />
        <Route path="/property/:id" element={<PropertyPage />} />
        <Route path="/profile" element={<ProfilePage />} /> {/* ✅ Новый маршрут */}
      </Routes>

      <Footer />
    </div>
  );
}

export default App;
