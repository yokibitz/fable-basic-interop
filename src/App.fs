module App

open Fable.Core
open Elmish
open Elmish.React
open System
open Feliz

JS.console.log "Hello from Fable!"

type State =
    { InputDimensions: Dimensions
      Board: P5.p5 }

type Msg =
    | InputWidth of float
    | InputHeight of float
    | InputResolution of int
    | SetDimensions of Dimensions

let init () =
    let initDimensions =
        { Width = 1000.
          Height = 800.
          Resolution = 5 }

    { InputDimensions = initDimensions
      Board =
          displayBoard
              { Width = initDimensions.Width
                Height = initDimensions.Height
                Resolution = initDimensions.Resolution } }

let update msg (state: State) =
    match msg with
    | InputWidth width ->
        let newDimension =
            { state.InputDimensions with
                  Width = width }

        { state with
              InputDimensions = newDimension }
    | InputHeight height ->
        let newDimension =
            { state.InputDimensions with
                  Height = height }

        { state with
              InputDimensions = newDimension }
    | InputResolution resolution ->
        let newDimension =
            { state.InputDimensions with
                  Resolution = resolution }

        { state with
              InputDimensions = newDimension }

    | SetDimensions dimensions ->
        state.Board.remove ()

        let newP5 = displayBoard dimensions

        { state with
              InputDimensions = dimensions
              Board = newP5 }

let render (state: State) (dispatch: Msg -> unit) =
    Html.div
        [ prop.children
            [ Html.div
                [ prop.children
                    [ Html.span "Width: "
                      Html.input
                          [ prop.valueOrDefault state.InputDimensions.Width
                            prop.onChange (float >> InputWidth >> dispatch) ] ] ]

              Html.div
                  [ prop.children
                      [ Html.span "Height: "
                        Html.input
                            [ prop.valueOrDefault state.InputDimensions.Height
                              prop.onChange (float >> InputHeight >> dispatch) ] ] ]

              Html.div
                  [ prop.children
                      [ Html.span "Resolution: "
                        Html.input
                            [ prop.valueOrDefault state.InputDimensions.Resolution
                              prop.onChange (int >> InputResolution >> dispatch) ] ] ]


              Html.button
                  [ prop.text "Apply"
                    prop.onClick (fun _ -> dispatch (SetDimensions state.InputDimensions)) ] ] ]




Program.mkSimple init update render
|> Program.withReactSynchronous "app"
|> Program.run
