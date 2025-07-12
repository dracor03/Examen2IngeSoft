<template>
  <div class="contenido-overlay">
    <div class="vending-wrapper">
      <h1 class="text-center pixel-title mb-5">Máquina Expendedora</h1>

      <div class="main-content">
        <!-- Lista de refrescos a la izquierda -->
        <div class="grid-vending">
          <div
            v-for="(drink, index) in drinks"
            :key="drink.name"
            class="drink-slot"
            @click="increaseQuantity(index)"
            title="Click para agregar uno"
          >
            <img
              :src="drink.image"
              :alt="drink.name"
              class="drink-image"
            />
            <div class="slot-info">
              <h5 class="pixel-text">{{ drink.name }}</h5>
              <p class="pixel-subtext">₡{{ drink.price }}</p>
              <p class="pixel-subtext">Stock: {{ drink.quantity }}</p>

              <input
                type="number"
                min="0"
                :max="drink.quantity"
                v-model.number="selectedQuantities[index]"
                @input="validateQuantity(index)"
                class="quantity-input"
                placeholder="Cantidad"
                @click.stop  
                inputmode="numeric" 
              />

              <p v-if="errors[index]" class="error-message">
                {{ errors[index] }}
              </p>
            </div>
          </div>
        </div>

        <!-- Carrito a la derecha -->
        <div class="cart-summary">
          <h2 class="pixel-title">Tu Compra</h2>
          <div v-if="cartItems.length === 0" class="empty-cart">No hay items seleccionados.</div>
          <ul v-else class="cart-list">
            <li v-for="item in cartItems" :key="item.name" class="cart-item">
              <span>{{ item.name }} x {{ item.quantity }}</span>
              <span>₡{{ item.quantity * item.price }}</span>
              <button class="btn-remove" @click="removeItem(item.name)">x</button>
            </li>
          </ul>

          <div class="total-container">
            <h3>Total: ₡{{ totalCost }}</h3>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import api from '../api'

const drinks = ref([])
const selectedQuantities = ref([])
const errors = ref([])

onMounted(async () => {
  const { data } = await api.get('/drinks')

  const imageMap = {
    'Coca Cola': '/assets/pixel/coca.png',
    'Pepsi': '/assets/pixel/pepsi.png',
    'Fanta': '/assets/pixel/fanta.png',
    'Sprite': '/assets/pixel/sprite.png'
  }

  drinks.value = data.map(d => ({
    ...d,
    image: imageMap[d.name] ?? '/assets/pixel/default.png'
  }))

  selectedQuantities.value = drinks.value.map(() => 0)
  errors.value = drinks.value.map(() => '')
})

function validateQuantity(index) {
  if (selectedQuantities.value[index] > drinks.value[index].quantity) {
    errors.value[index] = `Máximo disponible: ${drinks.value[index].quantity}`
    selectedQuantities.value[index] = drinks.value[index].quantity
  } else if (selectedQuantities.value[index] < 0) {
    errors.value[index] = 'Cantidad erronea'
    selectedQuantities.value[index] = 0
  } else {
    errors.value[index] = ''
  }
}

function increaseQuantity(index) {
  if (selectedQuantities.value[index] < drinks.value[index].quantity) {
    selectedQuantities.value[index]++
    errors.value[index] = ''
  }
}

function removeItem(name) {
  const index = drinks.value.findIndex(d => d.name === name)
  if (index !== -1) {
    selectedQuantities.value[index] = 0
    errors.value[index] = ''
  }
}

const cartItems = computed(() =>
  drinks.value
    .map((drink, idx) => ({ ...drink, quantity: selectedQuantities.value[idx] }))
    .filter(item => item.quantity > 0)
)

const totalCost = computed(() => {
  return cartItems.value.reduce((acc, item) => acc + item.price * item.quantity, 0)
})
</script>

<style scoped>
.contenido-overlay {
  background-color: rgba(255, 255, 255, 0.85);
  padding: 1rem;
  border-radius: 8px;
}

.vending-wrapper {
  max-width: 900px;
  margin: 0 auto;
}

.main-content {
  display: flex;
  gap: 1.5rem;
}

.grid-vending {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(140px, 1fr));
  gap: 1rem;
  justify-items: center;
  flex: 1;
  padding: 1rem;
  background-color: rgba(135, 206, 235, 0.2); /* celeste clarito */
  border-radius: 12px;
  box-shadow: inset 0 0 8px #00000033;
}

.drink-slot {
  background-color: var(--color-card-bg);
  border: 3px solid var(--color-border);
  border-radius: 8px;
  padding: 0.5rem;
  text-align: center;
  box-shadow: 3px 3px 0 var(--color-border);
  transition: transform 0.2s ease;
  width: 100%;
  cursor: pointer;
}

.drink-slot:hover {
  transform: scale(1.05);
}

.slot-info {
  margin-top: 0.5rem;
}

.drink-image {
  width: 64px;
  height: 64px;
  image-rendering: pixelated;
  object-fit: contain;
  margin-bottom: 0.5rem;
}

.quantity-input {
  width: 80px;
  padding: 4px;
  margin-top: 0.5rem;
  font-size: 14px;
  border: 1.5px solid var(--color-border);
  border-radius: 4px;
  text-align: center;
}

.error-message {
  color: red;
  font-size: 12px;
  margin-top: 0.25rem;
}

/* Carrito */
.cart-summary {
  width: 250px;
  background-color: rgba(135, 206, 235, 0.25);
  border-radius: 12px;
  padding: 1rem;
  font-family: 'Press Start 2P', cursive;
  box-shadow: inset 0 0 8px #00000033;
}

body {
  background-color: rgba(144, 238, 144, 0.3);
}

.pixel-title {
  color: var(--color-primary);
  font-size: 1.5rem;
  margin-bottom: 1rem;
}

.cart-list {
  list-style: none;
  padding: 0;
  margin: 0 0 1rem 0;
}

.cart-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0.3rem 0;
  border-bottom: 1px solid var(--color-border);
  font-size: 12px;
}

.btn-remove {
  background-color: var(--color-primary);
  border: none;
  color: white;
  font-weight: bold;
  cursor: pointer;
  border-radius: 4px;
  padding: 0 6px;
  font-size: 12px;
  transition: background-color 0.2s ease;
}

.btn-remove:hover {
  background-color: #e04e4e;
}

.empty-cart {
  font-size: 14px;
  font-style: italic;
  color: var(--color-border);
}

.total-container {
  text-align: center;
  color: var(--color-primary);
  font-weight: bold;
  font-size: 1.25rem;
}
</style>
