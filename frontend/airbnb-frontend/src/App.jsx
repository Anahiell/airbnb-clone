import { useState } from "react";
import { Routes, Route } from "react-router-dom";
import Header from "./components/Header";
import Footer from "./components/Footer";
import HomePage from "./pages/HomePage";
import Modal from "./components/Modal";
import SignInForm from "./components/SignInForm";
import ListingPage from "./pages/ListingPage";
import PropertyPage from "./pages/PropertyPage";

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);

  return (
    <div className="app">
      <Header onOpenModal={() => setIsModalOpen(true)} /> {/* Передаём функцию открытия */}

      {/* Модальное окно */}
      <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
        <SignInForm onSubmit={(data) => console.log("Вход:", data)} />
      </Modal>

      {/* Роутинг */}
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/listings" element={<ListingPage />} />
        <Route path="/property/:id" element={<PropertyPage />} />
      </Routes>

      <Footer />
    </div>
  );
}

export default App;
