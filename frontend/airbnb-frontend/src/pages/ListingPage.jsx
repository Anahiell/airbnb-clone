import { useState } from "react";
import PropertyCard from "../components/PropertyCard";
import SearchBar from "../components/SearchBar";
import CategoryFilters from "../components/CategoryFilters";
import styles from "../styles/ListingPage.module.css";

const allProperties = [
  { id: 1, image: "https://source.unsplash.com/300x200/?house", title: "Уютный домик у озера", location: "Берлин, Германия", price: "120€", rating: 4.8, category: "house" },
  { id: 2, image: "https://source.unsplash.com/300x200/?apartment", title: "Современные апартаменты", location: "Барселона, Испания", price: "90€", rating: 4.6, category: "apartment" },
  { id: 3, image: "https://source.unsplash.com/300x200/?villa", title: "Вилла с бассейном", location: "Миконос, Греция", price: "250€", rating: 4.9, category: "villa" },
  { id: 4, image: "https://source.unsplash.com/300x200/?cabin", title: "Горная хижина", location: "Альпы, Швейцария", price: "180€", rating: 4.7, category: "house" },
];

const ListingPage = () => {
  const [properties, setProperties] = useState(allProperties);
  const [selectedCategory, setSelectedCategory] = useState("all");

  const handleSearch = ({ location }) => {
    let filtered = allProperties.filter((p) =>
      p.location.toLowerCase().includes(location.toLowerCase())
    );
    if (selectedCategory !== "all") {
      filtered = filtered.filter((p) => p.category === selectedCategory);
    }
    setProperties(filtered);
  };

  const handleCategorySelect = (category) => {
    setSelectedCategory(category);
    let filtered = category === "all" ? allProperties : allProperties.filter((p) => p.category === category);
    setProperties(filtered);
  };

  return (
    <div className={styles.listingPage}>
      <h1>Все варианты жилья</h1>
      <SearchBar onSearch={handleSearch} />
      <CategoryFilters selectedCategory={selectedCategory} onSelectCategory={handleCategorySelect} />
      
      <div className={styles.grid}>
        {properties.length > 0 ? (
          properties.map((property) => <PropertyCard key={property.id} {...property} />)
        ) : (
          <p className={styles.noResults}>Ничего не найдено 😞</p>
        )}
      </div>
    </div>
  );
};

export default ListingPage;
