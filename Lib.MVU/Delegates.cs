using BlazorDSL;
using Microsoft.AspNetCore.Components;

namespace Lib.MVU;

/// <summary>
/// Erzeugt den initialien Zustand
/// </summary>
public delegate TState InitSimple<TState>();

/// <summary>
/// Erzeugt den initialien Zustand und einen Command für die Initialisierung
/// </summary>
public delegate (TState, Command<TMessage>) Init<TState, TMessage>();

/// <summary>
/// Versendet eine Nachricht um anschließend verarbeitet zu werden
/// </summary>
public delegate void Dispatch<TMessage>(TMessage message);

/// <summary>
/// Erzeugt einen Command, der ausgeführt wird
/// </summary>
/// <param name="dispatch">Methode zum Versenden von Nachrichten</param>
/// <returns></returns>
public delegate Task Command<TMessage>(Dispatch<TMessage> dispatch);

/// <summary>
/// Erzeugt einen Folgezustand aus dem aktuellen Zustand und einer Nachricht.
/// Hierbei handelt es sich immer um eine pure Methode
/// </summary>
/// <param name="state">Aktueller Zustand</param>
/// <param name="message">Nachricht</param>
/// <returns>Neuer Zustand / Folgezustand </returns>
public delegate TState UpdateSimple<TState, TMessage>(TState state, TMessage message);

/// <summary>
/// Erzeugt einen Folgezustand sowie einen Command aus dem aktuellen Zustand und einer Nachricht
/// </summary>
/// <param name="state">Aktueller Zustand</param>
/// <param name="message">Nachricht</param>
/// <returns>Neuer Zustand sowie ein Command</returns>
public delegate (TState state, Command<TMessage> command) Update<TState, TMessage>(TState state, TMessage message);

/// <summary>
/// Rendert die Oberfläche
/// </summary>
/// <param name="state">Aktueller Zustand</param>
/// <param name="dispatch">Methode zum Versenden einer Nachricht</param>
/// <param name="component">Instanz der Blazor Komponente</param>
/// <returns></returns>
public delegate Node View<TState, TMessage>(TState state, Dispatch<TMessage> dispatch, IComponent component);

public delegate Node ExecuteView(IComponent component);
public delegate void RenderComponent();
