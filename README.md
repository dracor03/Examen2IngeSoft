# Máquina Expendedora - Proyecto Backend y Frontend

Este proyecto simula una máquina expendedora de refrescos con gestión de inventario y cálculo de cambio. 

## Estructura

- **Backend**: API en .NET que gestiona el inventario de refrescos y monedas, y procesa compras.
- **Frontend**: Aplicación Vue 3 que permite seleccionar refrescos, insertar dinero y realizar compras.

---

## Requisitos Previos

- [.NET SDK 7.0 o superior](https://dotnet.microsoft.com/download)
- [Node.js y npm](https://nodejs.org/) para el frontend
- Navegador moderno para probar la aplicación frontend

---

## Backend

### Ejecutar la API

1. Abre una terminal en la carpeta del backend.
2. Restaurar paquetes:

   ```bash
   dotnet restore
   ```

3. Ejecutar la aplicación:

   ```bash
   dotnet run
   ```

4. La API estará disponible en `https://localhost:5001` o `http://localhost:5000` por defecto.

### Ejecutar tests con NUnit

1. Navega a la carpeta del proyecto de tests (si está separado) o la raíz si están integrados.
2. Ejecuta los tests con:

   ```bash
   dotnet test
   ```

   Verás el resultado de los tests en la consola.

---

## Frontend

### Instalar dependencias

Desde la carpeta del frontend, ejecuta:

```bash
npm install
```

### Ejecutar la aplicación Vue

```bash
npm run dev
```

Esto levantará un servidor local (usualmente en `http://localhost:5173`).

### Configuración

- Asegúrate de que el backend esté corriendo.
- La aplicación Vue hará peticiones al endpoint `/api/drinks` (puede necesitar configuración de proxy para CORS).

---

## Uso

- Selecciona las cantidades de refrescos que deseas comprar.
- Inserta dinero usando los botones de denominaciones.
- Haz clic en "Comprar".
- Si la compra es exitosa, verás el cambio devuelto y el inventario actualizado.

---

## Notas

- Actualmente el inventario está en memoria y se pierde al reiniciar la API.
- Validaciones básicas para cantidad y dinero insertado están implementadas.
- El cálculo de cambio usa la lógica de monedas disponibles.
- Puedes extender y mejorar la persistencia y validación según tus necesidades.

---

## Contacto

Para dudas o contribuciones, contacta al autor.

---

¡Gracias por usar esta máquina expendedora virtual! 🍹

