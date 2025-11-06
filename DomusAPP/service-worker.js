// Nombre de la caché (se recomienda cambiar este nombre cuando se actualicen los archivos)
const CACHE_NAME = 'domus-cache-v1';

// Lista de archivos a precachear (cuidado con las rutas relativas en ASP.NET)
// Debes listar aquí tus archivos más importantes, CSS, JS estáticos y las páginas
const urlsToCache = [
    '/', // Esto apunta a la raíz (probablemente Default.aspx si no hay redirección)
    '/Default.aspx',
    '/Site.Master', // O Site.Master si IIS lo sirve directamente
    // Si usas Telerik, es difícil cachear sus DLLs o recursos dinámicos, céntrate en tus archivos estáticos
    // Añade tu CSS y JS estáticos aquí:
     '/Content/global.css', 
     '/Content/global-fonts.css', 
    // '/Scripts/custom.js' 
];

// Evento de Instalación: Cachea los recursos estáticos
self.addEventListener('install', event => {
    // Espera hasta que el caché se abra y los archivos se almacenen
    event.waitUntil(
        caches.open(CACHE_NAME)
            .then(cache => {
                console.log('Service Worker: Cacheando archivos estáticos');
                return cache.addAll(urlsToCache).catch(error => {
                    console.error('Service Worker: Falló al cachear una URL:', error);
                    // Continúa con las URLs que sí pudo cachear
                });
            })
    );
});

// Evento de Activación: Limpia cachés antiguas
self.addEventListener('activate', event => {
    const cacheWhitelist = [CACHE_NAME];
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cacheName => {
                    if (cacheWhitelist.indexOf(cacheName) === -1) {
                        console.log('Service Worker: Eliminando caché antigua:', cacheName);
                        return caches.delete(cacheName);
                    }
                })
            );
        })
    );
});

// Estrategia de Red: Cache-first, con fallback a Network
self.addEventListener('fetch', event => {
    // Evitar interceptar peticiones no HTTP/HTTPS (como chrome-extension://)
    if (!event.request.url.startsWith('http')) {
        return;
    }

    // Estrategia: Cache, luego Network
    event.respondWith(
        caches.match(event.request)
            .then(response => {
                // Devuelve la versión en caché si existe
                if (response) {
                    return response;
                }

                // Si no está en caché, pide a la red
                return fetch(event.request).then(
                    networkResponse => {
                        // Verifica si recibimos una respuesta válida
                        if (!networkResponse || networkResponse.status !== 200 || networkResponse.type !== 'basic') {
                            return networkResponse;
                        }

                        // Clona la respuesta porque la original es un stream
                        const responseToCache = networkResponse.clone();

                        // Abre el caché y guarda la nueva respuesta
                        caches.open(CACHE_NAME)
                            .then(cache => {
                                // Solo cachear peticiones GET
                                if (event.request.method === 'GET') {
                                    cache.put(event.request, responseToCache);
                                }
                            });

                        return networkResponse;
                    }
                ).catch(error => {
                    // Manejo de errores de red (página sin conexión)
                    console.error('Fetch falló:', error);
                    // Aquí puedes devolver una página de error o sin conexión si la tienes en caché
                    // return caches.match('/offline.html'); 
                });
            })
    );
});
