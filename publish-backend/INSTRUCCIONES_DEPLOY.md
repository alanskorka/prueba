# 🚀 Instrucciones para Deploy en IIS - Día de la Defensa

## 📋 **Archivos Listos para Deploy:**

### **Frontend Angular (Listo):**
- ✅ Carpeta: `simulador-objetos-ui/dist/simulador-objetos-ui/browser/`
- ✅ Archivo `web.config` incluido
- ✅ Build de producción completado

### **Backend .NET (Preparar en IIS):**
- 📁 Proyecto: `src/SimuladorDeObjetos.WebApi/`
- 🔧 Necesita deploy en IIS

## 🎯 **Pasos para el Día de la Defensa:**

### **1. Deploy del Backend (.NET WebAPI):**
1. Abrir IIS Manager
2. Crear nuevo sitio web para la WebAPI
3. Configurar puerto (ej: 5038)
4. Deployar desde: `src/SimuladorDeObjetos.WebApi/`

### **2. Deploy del Frontend (Angular):**
1. Copiar carpeta `simulador-objetos-ui/dist/simulador-objetos-ui/browser/` a `C:\inetpub\wwwroot\`
2. Renombrar carpeta a algo como `simulador-objetos`
3. Crear sitio en IIS apuntando a esa carpeta
4. Configurar puerto (ej: 80)

### **3. Configurar IPs:**
1. Obtener IP de la máquina: `ipconfig`
2. Editar archivo `main-OS5TB774.js` en la carpeta del frontend
3. Buscar `http://localhost:5038/api` y cambiar por `http://[IP-MAQUINA]:5038/api`

### **4. Configurar Proxy:**
- En la máquina 2 (cliente), desactivar proxy
- Panel de Control → Internet Options → Connections → LAN Settings
- Desmarcar "Use a proxy server"

## 🔧 **Archivos Importantes:**
- `web.config` - Maneja routing de Angular
- `main-OS5TB774.js` - Contiene URLs del backend
- `index.csr.html` - Página principal

## 📞 **En caso de problemas:**
- Verificar que IIS URL Rewrite Module esté instalado
- Reiniciar sitios en IIS
- Verificar puertos no estén en uso
- Comprobar firewall de Windows

## ✅ **Checklist Final:**
- [ ] Backend corriendo en IIS
- [ ] Frontend corriendo en IIS  
- [ ] IPs configuradas correctamente
- [ ] Proxy desactivado en máquina cliente
- [ ] Aplicación accesible desde máquina 2

**¡Listo para la demo!** 🎉 