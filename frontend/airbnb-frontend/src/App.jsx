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
import RegisterForm from "./components/RegisterForm";
import { getUser, logout } from "./utils/auth";

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isRegistering, setIsRegistering] = useState(false);
  const navigate = useNavigate();
  const user = getUser();

  return (
    <div className="app">
      <Header onOpenModal={() => setIsModalOpen(true)} user={user} onLogout={logout} />

      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        {isRegistering ? (
          <RegisterForm onClose={() => setIsModalOpen(false)} toggleAuth={() => setIsRegistering(false)} />
        ) : (
          <SignInForm onClose={() => setIsModalOpen(false)} toggleAuth={() => setIsRegistering(true)} />
        )}
      </Modal>

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
